import { autoinject, bindable, observable } from "aurelia-framework";

import { AppRouter } from "../../routers/app-router";
import { Notifier, NotifierType } from "../../utilities/notifier";

import { Users } from "../../fake-user-list";

@autoinject
export class LoginLayout {
  constructor(private router: AppRouter, private notifier: Notifier) { }

  @observable loginUsers: Users.IUser[] = [];

  @bindable username: string;
  @bindable password: string;

  usernameChanged() {
    this.password = "";
  }

  async attached() {
    this.loginUsers = await Users.getUsers();
  }

  login() {
    if (!this.loginUsers.find(x => x.id === this.username)) {
      this.notifier.show("Error", "User not found", NotifierType.Danger, 3000);
      return;
    }

    this.notifier.show("Success", "Successfully logged in", NotifierType.Success, 3000);
    this.router.navigateToHome(this.username);
  }
}
