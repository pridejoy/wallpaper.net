import Vue from 'vue'

const moment = require('../utils/moment.min.js')


Vue.filter('Fdate', function(dataStr, pattern = 'YYYY/MM/DD') {
  return moment(dataStr).format(pattern)
})


Vue.filter('Ftime', function(dataStr, pattern = 'HH:mm') {
  return moment(dataStr).format(pattern)
})

Vue.filter('Fdatetwo', function(dataStr, pattern = 'YYYY-MM') {
  return moment(dataStr).format(pattern)
})

Vue.filter('Fdatethree', function(dataStr, pattern = 'YYYY-MM-DD') {
  return moment(dataStr).format(pattern)
})


Vue.filter('Fday', function(dataStr, pattern = 'M') {
  return moment(dataStr).format(pattern)+"日"
})
