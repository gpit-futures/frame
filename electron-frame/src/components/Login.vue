<template>
<v-content>
      <v-container fluid fill-height>
        <v-layout flex align-center justify-center>
          <v-flex xs2 sm2 align-content-center>
            <v-card color="white" class="black--text" d-flex>
              <v-card-title primary-title>
                <div style="width:100%;">
                  <h3 class="headline mb-0">Welcome</h3>
                  <div>Enter your credentials below to log in</div>
                <form>
                <v-select
                  v-model="user"
                  :items="loginUsers"
                  item-text="username"
                  label="Username"
                  color:="white"
                  attach
                  required
                ></v-select>
                <v-text-field
                  v-model="password"
                  :type="'password'"
                  label="Password"
                  required
                ></v-text-field>
                </form>
                </div>
              </v-card-title>
              <v-card-actions>
                <v-btn flat color="orange" @click="login()">Login</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
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
      test:["test","test","test"],
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
        this.$store.commit(mutators.SET_SHOW_DRAWER, true);
        this.$router.push({ name: "Home", params: { user: this.user } });
        // this.$router.push({ name: "Dashboard", params: { user: this.user } });
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
