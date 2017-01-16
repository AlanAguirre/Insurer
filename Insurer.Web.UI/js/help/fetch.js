import 'whatwg-fetch'

export const request = {
    call: (url, properties)=>{
        return fetch(url, properties)
        .then(checkStatus)
        .then(parseJSON)
    }
}


const checkStatus = (response) => {
    if (response.status >= 200 && response.status < 300) {
        return response
    } else {
        var error = new Error(response.statusText)
        error.response = response
        throw error
    }
}


const parseJSON = (response) => {
    return response.json()
}