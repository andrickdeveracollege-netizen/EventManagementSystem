const Auth = {
    get token() { return localStorage.getItem('em_token'); },
    get user() {
        try { return JSON.parse(localStorage.getItem('em_user') || 'null'); }
        catch (err) { return null; }
    },
    save(r) { localStorage.setItem('em_token', r.token); localStorage.setItem('em_user', JSON.stringify(r)); },
    clear() { localStorage.removeItem('em_token'); localStorage.removeItem('em_user'); },
    require(adminOnly = false) {
        if (!this.token) { location.href = '/index.html?signin=1&next=' + encodeURIComponent(location.pathname + location.search); return false; }
        if (adminOnly && !(this.user && this.user.isAdmin)) { location.href = '/dashboard.html'; return false; }
        return true;
    },
    logout() { this.clear(); location.href = '/index.html'; }
};

async function api(path, { method = 'GET', body, form } = {}) {
    const headers = {};
    if (Auth.token) headers['Authorization'] = 'Bearer ' + Auth.token;
    let payload;
    if (form) payload = form;
    else if (body !== undefined) { headers['Content-Type'] = 'application/json'; payload = JSON.stringify(body); }

    const res = await fetch(path, { method, headers, body: payload });
    if (res.status === 401) {
        Auth.clear();
        location.href = '/index.html?signin=1';
        throw new Error('Please sign in again.');
    }
    const text = await res.text();
    let data = null;
    if (text) {
        try { data = JSON.parse(text); }
        catch (err) { data = null; }
    }
    if (!res.ok) {
        let msg = (data && (data.message || data.title))
            || (res.status === 403 ? 'You are not allowed to do that.' : 'Request failed (' + res.status + ')');
        if (data && data.errors) msg = Object.values(data.errors).flat().join(' ');
        throw new Error(msg);
    }
    return data;
}

async function openEndorsement(id) {
    try {
        const res = await fetch(`/api/admin/requests/${id}/endorsement`, { headers: { Authorization: 'Bearer ' + Auth.token } });
        if (!res.ok) {
            const data = await res.json().catch(() => null);
            throw new Error((data && data.message) || 'Could not open the file.');
        }
        const name = decodeURIComponent(res.headers.get('X-Download-Name') || 'endorsement');
        const blob = await res.blob();
        const url = URL.createObjectURL(blob);
        if (blob.type === 'application/pdf' || blob.type.startsWith('image/')) window.open(url, '_blank');
        else { const a = document.createElement('a'); a.href = url; a.download = name; a.click(); }
    } catch (err) {
        toast(err.message, false);
    }
}

async function downloadFile(url) {
    const headers = Auth.token ? { Authorization: 'Bearer ' + Auth.token } : {};
    const res = await fetch(url, { headers });
    if (!res.ok) {
        const data = await res.json().catch(() => null);
        throw new Error((data && data.message) || 'Could not download the file (' + res.status + ').');
    }
    const m = /filename\*?=(?:UTF-8'')?"?([^";]+)"?/i.exec(res.headers.get('Content-Disposition') || '');
    const name = m ? decodeURIComponent(m[1]) : 'registrants.xlsx';
    const blobUrl = URL.createObjectURL(await res.blob());
    const a = document.createElement('a');
    a.href = blobUrl; a.download = name; document.body.appendChild(a); a.click(); a.remove();
    setTimeout(() => URL.revokeObjectURL(blobUrl), 4000);
}

async function openRegistrants(listUrl, exportUrl) {
    document.getElementById('regModal')?.remove();
    const modal = document.createElement('div');
    modal.className = 'modal open';
    modal.id = 'regModal';
    modal.innerHTML = '<div class="card" style="max-width:820px;width:100%"><div class="spinner">Loading…</div></div>';
    document.body.appendChild(modal);
    const close = () => modal.remove();
    modal.addEventListener('click', e => { if (e.target === modal) close(); });

    try {
        const d = await api(listUrl);
        const rows = d.rows.map((r, i) => `<tr><td>${i + 1}</td><td>${esc(r.fullName)}</td><td>${esc(r.email)}</td><td>${esc(r.level)}</td><td>${r.confirmed ? 'Yes' : 'No'}</td><td>${fmt(r.registeredAt)}</td></tr>`).join('');
        modal.querySelector('.card').innerHTML = `
      <h2 style="margin-top:0">Registered: ${esc(d.title)}</h2>
      <p class="muted">${fmt(d.startTime)} · <b>${d.rows.length}</b> of ${d.capacity} registered</p>
      <div style="overflow-x:auto;max-height:55vh;overflow-y:auto">
        ${d.rows.length ? `<table class="table"><thead><tr><th>#</th><th>Name</th><th>Email</th><th>Level</th><th>Confirmed</th><th>Registered on</th></tr></thead><tbody>${rows}</tbody></table>`
                : '<div class="empty-state">Nobody has registered yet.</div>'}
      </div>
      <div class="row" style="margin-top:14px">
        <button class="btn" id="regExport" type="button"${d.rows.length ? '' : ' disabled'}>Export to Excel</button>
        <button class="btn ghost" id="regClose" type="button">Close</button>
      </div>`;
        modal.querySelector('#regClose').onclick = close;
        modal.querySelector('#regExport').onclick = e => withBusy(e.target, () => downloadFile(exportUrl)).catch(err => toast(err.message, false));
    } catch (err) {
        modal.querySelector('.card').innerHTML = `<div class="empty-state">${esc(err.message)}</div><div class="row"><button class="btn ghost" id="regClose" type="button">Close</button></div>`;
        modal.querySelector('#regClose').onclick = close;
    }
}

const esc = s => String(s ?? '').replace(/[&<>"']/g, c => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));
const fmt = d => new Date(d).toLocaleString([], { dateStyle: 'medium', timeStyle: 'short' });
const fmtTime = d => new Date(d).toLocaleTimeString([], { hour: 'numeric', minute: '2-digit' });
const pad = n => String(n).padStart(2, '0');
const Range = {
    html(p, startLabel = 'Start', endLabel = 'End') {
        const today = isoLocal(new Date()).slice(0, 10);
        return `
      <div class="range-row">
        <div><label for="${p}StartDate">${startLabel} date</label><input type="date" id="${p}StartDate" min="${today}" /></div>
        <div><label for="${p}StartTime">${startLabel} time</label><input type="time" id="${p}StartTime" /></div>
      </div>
      <label class="inline-check"><input type="checkbox" id="${p}Same" checked /> ${endLabel} date is the same as the start date</label>
      <div class="range-row">
        <div><label for="${p}EndDate">${endLabel} date</label><input type="date" id="${p}EndDate" min="${today}" disabled /></div>
        <div><label for="${p}EndTime">${endLabel} time</label><input type="time" id="${p}EndTime" /></div>
      </div>`;
    },
    mount(p, startLabel, endLabel) {
        document.querySelector(`[data-range="${p}"]`).innerHTML = this.html(p, startLabel, endLabel);
        this.wire(p);
    },
    wire(p) {
        const same = document.getElementById(p + 'Same');
        const startDate = document.getElementById(p + 'StartDate');
        const endDate = document.getElementById(p + 'EndDate');
        const sync = () => {
            endDate.disabled = same.checked;
            if (same.checked) endDate.value = startDate.value;
        };
        same.onchange = sync;
        startDate.oninput = sync;
        startDate.onchange = sync;
        sync();
    },
    set(p, start, end) {
        const s = new Date(start), e = new Date(end);
        document.getElementById(p + 'StartDate').value = isoLocal(s).slice(0, 10);
        document.getElementById(p + 'StartTime').value = isoLocal(s).slice(11, 16);
        document.getElementById(p + 'EndDate').value = isoLocal(e).slice(0, 10);
        document.getElementById(p + 'EndTime').value = isoLocal(e).slice(11, 16);
        document.getElementById(p + 'Same').checked = s.toDateString() === e.toDateString();
        this.wire(p);
    },
    read(p) {
        const g = id => document.getElementById(p + id).value;
        if (!g('StartDate')) return { error: 'Please set the start date.' };
        if (!g('StartTime')) return { error: 'Please set the start time.' };
        if (!g('EndTime')) return { error: 'Please set the end time.' };
        const endDate = document.getElementById(p + 'Same').checked ? g('StartDate') : g('EndDate');
        if (!endDate) return { error: 'Please set the end date.' };
        const start = new Date(g('StartDate') + 'T' + g('StartTime'));
        const end = new Date(endDate + 'T' + g('EndTime'));
        if (end <= start) return { error: 'The end must be after the start.' };
        return { start, end };
    }
};

const isoLocal = d => `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}:00`;
const statusTag = s => `<span class="tag ${String(s).toLowerCase()}">${esc(s)}</span>`;
const placesOf = e => e.facilityNames.length ? e.facilityNames.join(', ') : 'Equipment only';
const privateTag = e => e.showOnBulletin ? '' : ' <span class="tag private">Private · not on bulletin</span>';

// One office's decision, as the requester sees it. Refused equipment does not stop the event.
function officeLine(x) {
    const refused = '<span class="tag partial">Equipment refused</span>';
    const tags = !x.equipmentRejected ? statusTag(x.status)
        : x.status === 'Approved' ? statusTag('Venue approved') + refused
        : refused;
    const why = x.rejectionReason ? ' — ' + esc(x.rejectionReason) : '';
    const after = x.equipmentRejected ? ' <span class="muted">(the event can go on without it)</span>' : '';
    const note = x.note ? `<div class="hint-box" style="margin:4px 0"><b>Note from ${esc(x.office)}:</b> ${esc(x.note)}</div>` : '';
    return `<div class="muted">${esc(x.office)}: ${tags}${why}${after}</div>${note}`;
}

// Link that the organizer of a private event shares. Events open to outsiders get the guest form;
// signed-in school users who open it still register with their account.
const registrationLink = e => `${location.origin}/register.html?eventId=${e.id}${String(e.targetAudience).includes('Outsider') ? '&guest' : ''}`;

async function copyText(text) {
    try { await navigator.clipboard.writeText(text); toast('Link copied. Send it to the people you invite.'); }
    catch (err) { window.prompt('Copy this link:', text); }
}

function showMsg(el, text, ok) { el.textContent = text; el.className = 'msg ' + (ok ? 'ok' : 'err'); }

function toast(text, ok = true) {
    const el = document.createElement('div');
    el.className = 'toast' + (ok ? '' : ' err');
    el.textContent = text;
    document.body.appendChild(el);
    setTimeout(() => el.remove(), 4500);
}

async function withBusy(btn, work) {
    const label = btn.textContent;
    btn.disabled = true;
    btn.textContent = 'Please wait…';
    try { return await work(); }
    finally { btn.disabled = false; btn.textContent = label; }
}

function confirmLogout() {
    return new Promise(resolve => {
        const modal = document.createElement('div');
        modal.className = 'logout-modal';
        modal.innerHTML = `
      <div class="logout-dialog" role="dialog" aria-modal="true" aria-labelledby="logoutTitle">
        <div class="logout-icon">!</div>
        <h2 id="logoutTitle">Sign out?</h2>
        <p>Are you sure you want to sign out?</p>
        <div class="logout-actions">
          <button type="button" class="logout-cancel">Cancel</button>
          <button type="button" class="logout-confirm">Sign out</button>
        </div>
      </div>`;
        document.body.appendChild(modal);

        const close = result => { modal.remove(); resolve(result); };
        modal.querySelector('.logout-cancel').onclick = () => close(false);
        modal.querySelector('.logout-confirm').onclick = () => close(true);
        modal.onclick = e => { if (e.target === modal) close(false); };
    });
}

function renderNav(el) {
    const brand = document.querySelector('header.top .brand');
    if (brand && !brand.querySelector('.brand-logo')) {
        brand.insertAdjacentHTML('afterbegin', '<img class="brand-logo" src="/images/angelicum-logo.png" alt="UST Angelicum College logo" />');
    }

    const u = Auth.user;
    if (!u) {
        el.innerHTML = '<a href="/visitor.html">Visitors</a>';
        markActive(el);
        return;
    }

    const dashboard = u.isAdmin ? '' :
        `<div class="nav-dropdown"><a class="nav-dropdown-toggle" href="/dashboard.html">Dashboard ▾</a><div class="nav-dropdown-menu"><a href="/register.html">Register for an Event</a><a href="/request.html">Request an Event</a></div></div>`;
    const myEvents = u.isAdmin ? '' : '<a href="/myevents.html">My Events</a>';
    el.innerHTML = `<a href="/index.html">Bulletin</a>` + myEvents + dashboard +
        (u.isAdmin ? '<a href="/admin.html">Approvals</a>' : '') +
        (u.isSuperAdmin ? '<a href="/superadmin.html">Super admin</a>' : '') +
        `<a href="#" id="logoutLink">Sign out</a>`;
    document.getElementById('logoutLink').onclick = async e => {
        e.preventDefault();
        if (await confirmLogout()) Auth.logout();
    };
    markActive(el);
}

function markActive(el) {
    const path = location.pathname === '/myregistrations.html' ? '/myevents.html' : location.pathname;
    for (const a of el.querySelectorAll('a')) {
        if (a.href && new URL(a.href).pathname === path) a.classList.add('active');
    }
}
