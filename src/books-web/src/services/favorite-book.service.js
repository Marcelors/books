import * as base from './base.service'

const urlBase = "v1/favorite-books";

const get = (filter) => {
    return base.get(urlBase, filter);
}

const remove = (id) => {
    return base.remove(urlBase, id);
}

const add = (model) => {
    return base.post(urlBase, model);
}

export {
    get,
    add,
    remove
}