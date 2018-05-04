import { autoinject } from "aurelia-framework";
import { Router, RouterConfiguration } from "aurelia-router";

import { AppRouter } from "./routers/app-router";

@autoinject
export class App {
  constructor(private appRouter: AppRouter ) {

  }

  configureRouter(config: RouterConfiguration, router: Router) {
    this.appRouter.configure(config, router);
  }
}
