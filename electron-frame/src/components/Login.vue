<template>
  <div class="dw-login-container columns is-tablet is-centered">
    <div class="column is-one-third-tablet is-narrow">
      <div class="box">
        <div class="field">
          <label class="label">Username</label>
          <div class="control">
            <div class="select is-fullwidth"  v-bind:class="{'is-loading': loadingUsers}">
              <select v-model="user">
                <option value="">Select user</option>
                <option v-for="user in loginUsers" :key="user.id" v-bind:value="user">{{user.username}}</option>
              </select>
            </div>
          </div>
        </div>
        <div class="field">
          <label class="label">Password</label>
          <div class="control">
            <input type="password" class="input" v-model="password" placeholder="Password">
          </div>
        </div>
        <div class="control">
          <button class="button is-success is-fullwidth" @click="login()">Login</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { UserService } from '../services/user-service'
import { Notifier, NotifierType } from "../utilities/notifier";
import mutators from '../store/mutators'

export default {
  name: 'login',
  components: { },
  data () {
    return { // Some mock data to fill the page
      loginUsers: [],
      password: '',
      userName: '',
      user: {},
      loadingUsers: true,
      notifier: new Notifier()
    }
  }, 
  methods: {
    login() {
      console.log('test: ' + this.user)
      this.notifier.show("Success", "Successfully logged in", NotifierType.Success, 3000);
      this.$store.commit(mutators.SET_USER, this.user)
      this.$router.push({name: 'Home', params: {user: this.user}})
    }
  },
  async mounted () {
    let userService = new UserService();
    this.loginUsers = await userService.getUsers();
    this.loadingUsers = false;
    console.log(this.loginUsers)
  }
}
</script>
