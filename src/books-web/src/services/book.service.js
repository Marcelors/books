import * as base from './base.service'

const urlBase = "v1/books";

const get = (filter) => {
    return base.get(urlBase, filter);
}

export {
    get
}