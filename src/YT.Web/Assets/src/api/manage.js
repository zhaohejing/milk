import fetch from 'utils/fetch';
export function getRoles(data) {
  return fetch({
    url: '/api/services/app/role/GetRoles',
    method: 'post',
    data
  });
}
export function getUsers(data) {
  return fetch({
    url: '/api/services/app/user/GetUsers',
    method: 'post',
    data
  });
}

export function getInfo() {
  return fetch({
    url: '/api/services/app/session/GetCurrentLoginInformations',
    method: 'post'
  });
}

