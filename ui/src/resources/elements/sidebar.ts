import { bindable, bindingMode } from "aurelia-framework";

export class SidebarCustomElement {
  @bindable({ defaultBindingMode: bindingMode.twoWay }) selected: string;
  @bindable clients: any[];

  select(clientId: string) {
    this.selected = clientId;
  }
}
