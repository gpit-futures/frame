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
import { UserService } from "../services/user-service";
import { Notifier, NotifierType } from "../utilities/notifier";
import mutators from "../store/mutators";
import axios from "axios";

export default {
  name: "login",
  components: {},
  data() {
    return {
      // Some mock data to fill the page
      loginUsers: [],
      password: "",
      userName: "",
      user: {},
      loadingUsers: true,
      notifier: new Notifier()
    };
  },
  methods: {
    async login() {
      console.log(this.token);
      this.$store.commit(
        mutators.SET_TOKEN,
        await this.obtainAccessToken(this.user.username, this.password)
      );
      console.log(this.token);

      if (this.token == null) {
        this.notifier.show(
          "Warning",
          "Login failed",
          NotifierType.Warning,
          3000
        );
      } else {
        this.notifier.show(
          "Success",
          "Successfully logged in",
          NotifierType.Success,
          3000
        );
        this.$store.commit(mutators.SET_USER, this.user);
        this.$router.push({ name: "Home", params: { user: this.user } });
      }
    },
    async obtainAccessToken(username, password) {
      let json;
      const params = new URLSearchParams();
      params.append("username", username);
      params.append("password", password);
      params.append("client_id", "fooClientIdPassword");
      params.append("client_secret", "fred");
      params.append("grant_type", "password");
      console.log(username + " " + password);

      await axios
        .post(
          "http://ec2-18-130-14-227.eu-west-2.compute.amazonaws.com/oauth/token",
          params,
          {
            headers: {
              "Content-type":
                "application/x-www-form-urlencoded; charset=utf-8",
              Authorization: "Basic " + btoa("fooClientIdPassword::fred")
            }
          }
        )
        .then(res => {
          console.log(res);
          json = res.data;
        })
        .catch(error => {
          console.error(error);
          json = null;
        });
      return json;
    }
  },
  computed: {
    token() {
      return this.$store.state.token;
    }
  },
  async mounted() {
    let userService = new UserService();
    this.loginUsers = await userService.getUsers();
    this.loadingUsers = false;
    console.log(this.loginUsers);
  }
};
</script>
