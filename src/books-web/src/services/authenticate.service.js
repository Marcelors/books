import * as base from './base.service'

const urlBase = "v1/authenticate";

const authenticate = (form) => {
    return base.post(urlBase, form);
}

export {
    authenticate
}