import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from './components/Login'
import Home from './components/Home'
import Dashboard from "./components/Dashboard.vue"

Vue.use(VueRouter)

const gotoLanding = function (to, from, next) {
  next({name: 'Login'})
}

const routes = [
  { name: 'Dashboard', path: '/dashboard/:user', component: Dashboard, props: true },
  { name: 'Home', path: '/home/:user', component: Home, props: true },
  { name: 'Login', path: '/login', component: Login },
  { path: '*', beforeEnter: gotoLanding }
]

export default new VueRouter({
  routes
})
