import { autoinject, bindable } from "aurelia-framework";

@autoinject
export class PatientBannerCustomElement {
  constructor(private el: Element) {
    el.classList.add("dw-patient-banner", "columns", "is-gapless");
  }

  @bindable name: string;
  @bindable title: string;
  @bindable deceased: boolean = false;

  attached() {
    if (this.deceased) {
      this.el.classList.add("is-deceased");
    }
  }
}
