import * as base from './base.service'

const urlBase = "v1/users";

const get = (filter) => {
    return base.get(urlBase, filter);
}

const remove = (id) => {
    return base.remove(urlBase, id);
}

const add = (model) => {
    return base.post(urlBase, model);
}

const update = (id, model) => {
    return base.put(urlBase, id, model);
}

export {
    get,
    add,
    remove,
    update
}