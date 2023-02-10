import CryptoJS from 'crypto-js'
const key = '8080808080808080'
const iv = '8080808080808080'
export default ({ app }, inject) => {
  function aesEncrypt(txt) {
    const cipher = CryptoJS.AES.encrypt(txt, CryptoJS.enc.Utf8.parse(key), {
      iv: CryptoJS.enc.Utf8.parse(iv),
      mode: CryptoJS.mode.CBC,
    })
    return cipher.toString()
  }inject("aesEncrypt", aesEncrypt)
}
