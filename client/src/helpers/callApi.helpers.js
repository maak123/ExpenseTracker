import {baseUrl} from '../config/core.config'

const getFullUrl=(suffix)=>{
    return `${baseUrl}/${suffix}`
}


const getHeader=()=>{
    const headers = {
        accept: 'application/json',
        'Content-Type': 'application/json',
      };
}


export {
    getHeader,
    getFullUrl
  };