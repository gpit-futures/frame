import { autoinject, bindable, observable } from "aurelia-framework";

@autoinject
export class FramesCustomElement {
  constructor(private el: Element) { }

  @bindable selected: string;
  @bindable clients: any[];

  clientsChanged() {
    if (!this.clients || this.clients.length === 0) {
      return;
    }

    this.selected = this.clients[0].id;
  }
}
