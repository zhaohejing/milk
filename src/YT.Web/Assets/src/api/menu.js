import fetch from 'utils/fetch';
export function userMenus() {
  return fetch({
    url: '/api/Account/Authenticate',
    method: 'post'
  });
}

export function allMenus() {
  return fetch({
    url: '/login/logout',
    method: 'post'
  });
}

export function editMenus(menuDto) {
  return fetch({
    url: '/api/services/app/session/GetCurrentLoginInformations',
    method: 'post',
    data: menuDto
  });
}
export function addMenus(menuDto) {
  return fetch({
    url: '/api/services/app/session/GetCurrentLoginInformations',
    method: 'post',
    data: menuDto
  });
}
