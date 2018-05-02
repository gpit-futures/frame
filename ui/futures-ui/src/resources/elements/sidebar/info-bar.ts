import { bindable, observable } from "aurelia-framework";

export class InfoBarCustomElement {
  constructor() {
  }

  private timeInterval: number;
  
  @observable time: Date = new Date();
  @bindable practitionerName: string;

  attached() {
    this.timeInterval = window.setInterval(() => {
      this.time = new Date();
    }, 1000);
  }

  detached() {
    window.clearInterval(this.timeInterval);
  }
}
