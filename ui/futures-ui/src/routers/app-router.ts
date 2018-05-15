import { Router, RouterConfiguration } from "aurelia-router";
import { PLATFORM } from "aurelia-pal";

export class AppRouter {
  private router: Router;

  configure(config: RouterConfiguration, router: Router) {
    this.router = router;

    config.title = "GP IT";
    config.map([
      {
        route: "", redirect: "login"
      },
      {
        route: "home/:user", name: "home", moduleId: PLATFORM.moduleName("pages/home/home-layout")
      },
      {
        route: "login", name: "login", moduleId: PLATFORM.moduleName("pages/login/login-layout"), title: "Login"
      }
    ]);
  }

  navigateToHome(user: string) {
    this.router.navigateToRoute("home", { user: user });
  }
}
