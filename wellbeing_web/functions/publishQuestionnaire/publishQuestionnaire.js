// Docs on event and context https://docs.netlify.com/functions/build/#code-your-function-2
const axios = require('axios')
exports.handler = async (event) => {
  try {
    const wellbeing_api = 'https://iq0t7hfiqe.execute-api.eu-west-1.amazonaws.com/api/wellbeing/' 
    const token = event.headers["authorization"]
    const requestBody = event.body ? JSON.parse(event.body) : event
    await axios.post(wellbeing_api, requestBody, {
      method: 'POST',
      headers: { Authorization: token },
    })
    return {statusCode: 200,}
  } catch (error) {
    return { statusCode: 500, body: error.toString() }
  }
}