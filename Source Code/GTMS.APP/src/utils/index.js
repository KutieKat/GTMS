import moment from 'moment'

export const isEmpty = obj => {
  for (let key in obj) {
    if (obj.hasOwnProperty(key)) { return false }
  }
  return true
}

export const formatDateString = (dateString, timeIncluded = true) => {
  if (timeIncluded) {
    return moment(dateString).format('hh:mm, DD-MM-YYYY')
  }
  return moment(dateString).format('DD-MM-YYYY')
}

export const getValue = (value, defaultValue = '(Chưa có dữ liệu)') => {
  if (value !== '') {
      return value
  }
  else {
      return defaultValue
  }
}

export const excelFormat = {
  normalFont: { name: 'Times New Romans', size: 14 },
  boldFont: { name: 'Times New Romans', size: 14, bold: true },
  italicFont: { name: 'Times New Romans', size: 14, italic: true },
  border: { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } },
  center: { vertical: 'middle', horizontal: 'center' },
  left: { vertical: 'middle', horizontal: 'left' },
  right: { vertical: 'middle', horizontal: 'right' }
}

export const isValidEmail = (email) => {
  let pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i)
  return pattern.test(email)
}

export const isValidPhoneNumber = (phone) => {
  let flag = false;
  phone = phone.replace('(+84)', '0')
  phone = phone.replace('+84', '0')
  phone = phone.replace('0084', '0')
  phone = phone.replace(/ /g, '')

  if (phone != '') {
      let firstNumber = phone.substring(0, 2);
      if ((firstNumber == '09' || firstNumber == '08') && phone.length == 10) {
          if (phone.match(/^\d{10}/)) {
              flag = true
          }
      } else if (firstNumber == '01' && phone.length == 11) {
          if (phone.match(/^\d{11}/)) {
              flag = true
          }
      }
  }
  return flag;
}

const numberToWord = (decimalScore, isWholeNumberPart) => {
  switch (decimalScore)
  {
      case '0': if (isWholeNumberPart) return "không";
              return "chẵn";
      break;
      case '1': return "một";
      break;
      case '2': return "hai";
      break;
      case '3': return "ba";
      break;
      case '4': return "bốn";
      break;
      case '5': return "năm";
      break;
      case '6': return "sáu";
      break;
      case '7': return "bảy";
      break;
      case '8': return "tám";
      break;
      case '9': return "chín";
      break;
  }
}

export const standardizeScore = (numberScore) => {
  var stringScore = new String(numberScore.toString());
  var wholeNumberPart = stringScore.charAt(0);
  var numberPart = stringScore.charAt(2);
  var wordScore = "";
  if (numberPart == "") numberPart = '0';

  if (numberPart == '0') 
  { 
  wordScore = numberToWord(wholeNumberPart, true) + " " + numberToWord(numberPart, false);
  }
  else wordScore = numberToWord(wholeNumberPart, true) + " phẩy " + numberToWord(numberPart, false);

  return wordScore.charAt(0).toUpperCase() + wordScore.slice(1, wordScore.length);
}