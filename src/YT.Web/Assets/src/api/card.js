import fetch from 'utils/fetch';
export function getCards(data) {
  return fetch({
    url: '/api/services/app/customer/GetPagedCustomersAsync',
    method: 'post',
    data
  });
}

export function updateCard(data) {
  return fetch({
    url: '/api/services/app/customer/CreateOrUpdateCustomerAsync',
    method: 'post',
    data
  });
}

export function getCardForEdit(data) {
  return fetch({
    url: '/api/services/app/customer/GetCustomerForEditAsync',
    method: 'post',
    data
  });
}
export function deleteCard(data) {
  return fetch({
    url: '/api/services/app/customer/DeleteCustomerAsync',
    method: 'post',
    data
  });
}