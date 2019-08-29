<template>
<v-content>
      <v-container fluid fill-height>
        <v-layout flex align-center justify-center>
          <v-flex xs2 sm2 align-content-center>
            <v-card color="white" class="black--text" d-flex>
              <v-form>
              <v-card-title primary-title>
                <div style="width:100%;">
                  <h3 class="headline mb-0">Welcome</h3>
                  <div>Enter your credentials below to log in</div>
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
                  v-on:keyup.enter="login()"
                ></v-text-field>
                </div>
              </v-card-title>
              <v-card-actions>
                <v-btn flat color="orange" @click="login()">Login</v-btn>
              </v-card-actions>
              </v-form>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
</template>

<script>
import { UserService } from "../services/user-service";
import { NotificationService } from "../services/notification-service";
import { Notifier, NotifierType } from "../utilities/notifier";
import mutators from "../store/mutators";
import axios from "axios";
import { HubConnectionBuilder } from "@aspnet/signalr";

export default {
  name: "login",
  components: {},
  data() {
    return {
      // Some mock data to fill the page
      loginUsers: [],
      test: ["test", "test", "test"],
      password: "",
      userName: "",
      user: {},
      loadingUsers: true,
      notifier: new Notifier(),
      connection: null,
      userService: new UserService(),
      notificationService: new NotificationService(),
      frameworkBackendUrl : "localhost:8080"
    };
  },
  methods: {
    async login() {
      this.$store.commit(mutators.SET_TOKEN, await this.obtainAccessToken(this.user.username, this.password));

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
        this.$store.commit(mutators.SET_GP, `${this.user.title} ${this.user.firstName} ${this.user.lastName}`);
        this.$store.commit(mutators.SET_SHOW_DRAWER, true);
        this.$router.push({ name: "Home", params: { user: this.user } });

        // SETUP NOTIFICATIONS
        this.$store.commit(mutators.SET_NOTIFICATIONS, await this.notificationService.getNotifications(this.token.access_token));

        // Toast Notifications
        // Deal with connection to signalR for notifications
        const signalR = require("@aspnet/signalr");
        this.connection = new signalR.HubConnectionBuilder()
          .withUrl(
            `${this.frameworkBackendUrl}/ws/notifications`,
            {
              accessTokenFactory: () => this.$store.state.token.access_token
            }
          )
          .build();

        this.connection.on("notification", data => {
          this.triggerNotificationRefresh(data);
        });
        this.connection.start();
      }
    },
    async triggerNotificationRefresh(data) {
      this.$store.commit(mutators.SET_NOTIFICATIONS, await this.notificationService.getNotifications(this.token.access_token));
      this.notifier.show(
        `${data.type} - ${this.notifications[0].patientName}`,
        data.summary,
        NotifierType.Info,
        8000
      );
    },
    logout() {
      this.connection.stop();
    },
    async obtainAccessToken(username, password) {
      let json;
      const params = new URLSearchParams();
      params.append("username", username);
      params.append("password", password);
      params.append("client_id", "fooClientIdPassword");
      params.append("client_secret", "fred");
      params.append("grant_type", "password");

      await axios
        .post(
          "http://localhost/oauth/token",
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
    },
    notifications() {
      return this.$store.state.notifications;
    }
  },
  async mounted() {
    this.loginUsers = await this.userService.getUsers();
    this.loadingUsers = false;

    this.$root.$on("notifications", () => {
      this.logout();
    });
  }
};
</script>
