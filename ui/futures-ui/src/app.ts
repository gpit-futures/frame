import { autoinject } from "aurelia-framework";
import { Router, RouterConfiguration } from "aurelia-router";

import { AppRouter } from "./routers/app-router";
import { Notifier, NotifierType } from "./utilities/notifier";

@autoinject
export class App {
  constructor(private appRouter: AppRouter, private notifier: Notifier) {

  }

  configureRouter(config: RouterConfiguration, router: Router) {
    this.appRouter.configure(config, router);
  }
}
