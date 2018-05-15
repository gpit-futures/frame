import { autoinject, bindable, observable } from "aurelia-framework";

@autoinject
export class PatientBannerSupplementCustomElement {
  constructor(private el: Element) { }

  private toggle = (ev: Event) => {
    this.expanded = !this.expanded;
  }

  @observable expanded: boolean = false;
  @bindable address;
  @bindable contact: string;

  attached() {
    this.el.addEventListener("click", this.toggle);
  }

  detached() {
    this.el.removeEventListener("click", this.toggle);
  }
}
