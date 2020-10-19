import axios from 'axios'
import Qs from "qs"

const urlBase = "http://localhost:5000/"

const instance = axios.create({
    timeout: 60000,
    headers: { 'Content-Type': 'application/json' },
});

instance.interceptors.request.use(
    (config) => {
        let token = localStorage.getItem("token")

        if (token != null && token != undefined && token !== "") {
            config.headers['Authorization'] = `Bearer ${token}`
            return config;
        }

        return config
    },
    (error) => {
        return Promise.reject(error)
    }
)

const post = (url, data, timeout, headers) => {
    return instance.post(`${urlBase}${url}`, data, {
        timeout: timeout == undefined || timeout == null ? instance.timeout : timeout,
        headers: Object.assign({}, instance.headers, {
            ...headers
        })
    }).then(result => {
        return result.data.data;
    }).catch(error => {
        getError(error)
    });
}

const remove = (url, id) => {
    if (id == undefined || id == null) {
        return instance.delete(`${urlBase}${url}`).then(result => {
            return result.data.data;
        }).catch(error => {
            getError(error)
        });
    } else {
        return instance.delete(`${urlBase}${url}/${id}`).then(result => {
            return result.data.data;
        }).catch(error => {
            getError(error)
        });
    }
}

const removeByParams = (url, params) => {

    return instance.delete(`${urlBase}${url}`, {
        params: params,
        paramsSerializer: (params) => {
            return Qs.stringify(params, { arrayFormat: 'repeat' })
        }
    }).then(result => {
        return result.data.data;
    }).catch(error => {
        getError(error)
    });
}


const getByParams = (url, params) => {
    return instance.get(`${urlBase}${url}`, {
        params: params,
        paramsSerializer: (params) => {
            return Qs.stringify(params, { arrayFormat: 'repeat' })
        }
    }).then(result => {
        return result.data.data;
    }).catch(error => {
        getError(error)
    });
}


const put = (url, id, data) => {
    if (id == undefined || id == null) {
        return instance.put(`${urlBase}${url}`, data).then(result => {
            return result.data.data;
        }).catch(error => {
            getError(error)
        });
    } else {
        return instance.put(`${urlBase}${url}/${id}`, data).then(result => {
            return result.data.data;
        }).catch(error => {
            getError(error)
        });
    }
}

const get = (url, params) => {
    return instance.get(`${urlBase}${url}`, {
        params: params
    }).then(result => {
        return result.data.data;
    }).catch(error => {
        getError(error)
    });
}

const getFile = (url, params) => {
    return instance.get(`${urlBase}${url}`, {
        responseType: 'blob',
        params: params,
        paramsSerializer: (params) => {
            return Qs.stringify(params, { arrayFormat: 'repeat' })
        }
    }).then(result => {
        const contentDisposition = result.headers['content-disposition'];
        let fileName = 'unknown';
        if (contentDisposition) {
            const fileNameMatch = contentDisposition.match(/filename="(.+)"/);
            if (fileNameMatch.length === 2)
                fileName = fileNameMatch[1];
        }
        return { data: result.data, name: fileName };
    }).catch(error => {
        getError(error)
    });
}


const all = (...get) => {
    return axios.all(get)
        .then(result => {
            return result
        })
        .catch(error => {
            getError(error)
        })
}

const getError = (error) => {

    if (typeof error === "string") {
        throw error
    }
    if (error.response == null || error.response == undefined) {
        throw error.message
    }


    if (error.response.data.errors != null && error.response.data.errors != undefined) {
        var err = "";

        Object.keys(error.response.data.errors).map(function (key) {
            if (err === "") {
                err = error.response.data.errors[key][0]
            } else {
                err += ", ";
                err += error.response.data.errors[key][0]
            }
        });

        throw err
    }

    if (error.response.data == null || error.response.data == undefined) {
        throw error.message
    }

    if (typeof error.response.data === "string") {
        throw error.message
    }


    throw error.response.data.message
}

export {
    post,
    remove,
    removeByParams,
    put,
    get,
    getFile,
    all,
    getByParams
}