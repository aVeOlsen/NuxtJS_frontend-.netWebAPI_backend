export const getData = async function (url, axios) {
  let response = await axios.get(url)
  return {
    data: response.data.data,
    included: response.data.included,
  }
}

export const postData = async function (url, axios) {
  let response = await axios.post(url)
  return {
    data: response.data.data,
    included: response.data.included,
  }
}
