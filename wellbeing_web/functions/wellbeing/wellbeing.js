// require('dotenv').config();
const axios = require('axios')
exports.handler = async (event) => {
  try {
    const wellbeing_api = 'https://iq0t7hfiqe.execute-api.eu-west-1.amazonaws.com/api/wellbeing' //'http://localhost:5000/api/wellbeing'//
    const requestBody = event.body ? JSON.parse(event.body) : event
    const token = requestBody
    const response = await axios.get(wellbeing_api, {
      metod: 'GET',
      headers: { Authorization: 'Bearer ' + token },
    }) 
      
      const data = response.data
    return {
      statusCode: 200,
      body: JSON.stringify({ data }),
    }
  } 
  catch (error) {
    return { statusCode: 500, body: JSON.stringify(error) }
  }
}
