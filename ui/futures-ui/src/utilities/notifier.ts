export enum NotifierType {
  Info = <any>"is-info",
  Success = <any>"is-success",
  Warning = <any>"is-warning",
  Danger = <any>"is-danger"
}
export class Notifier {
  constructor() {
    this.createContainer();
  }

  show(title: string, message: string, type: NotifierType = NotifierType.Info, timeout: number = 0) {
    const date = new Date();
    const ntfId = `notifier-${date.getTime()}`;

    let container = document.querySelector(".notifier-container");

    if (!container) {
      this.createContainer();
      container = document.querySelector(".notifier-container");
    }

    const ntf = this.createElement("div", { class: `notification notifier ${type}` });
    const ntfBody = this.createElement("div", { class: "notifier-body" });
    const ntfClose = this.createElement("button", { class: "delete", type: "button" });

    ntfBody.innerText = message;
    ntfClose.innerHTML = "&times";

    ntf.appendChild(ntfClose);

    if (title || `${title}`.trim() !== "") {
      const ntfTitle = this.createElement("h2", { class: "subtitle is-marginless" });
      ntfTitle.innerText = title;
      ntf.appendChild(ntfTitle);
    }

    ntf.appendChild(ntfBody);

    container.appendChild(ntf);

    setTimeout(() => {
      ntf.classList.add("shown");
      ntf.setAttribute("id", ntfId);
    }, 100);

    if (timeout > 0) {
      setTimeout(() => this.hide(ntfId), timeout);
    }

    ntfClose.addEventListener("click", () => this.hide(ntfId));

    return ntfId;
  }

  hide(notificationId: string): boolean {
    const ntf = document.querySelector(`#${notificationId}`);

    if (ntf) {
      ntf.classList.remove("shown");

      setTimeout(() => {
        ntf.parentNode.removeChild(ntf);
      }, 600);

      return true;
    }

    return false;
  }

  private createElement(element: string, attributes: object): HTMLElement {
    const el = document.createElement(element);
    
    for (const prop in attributes) {
      el.setAttribute(prop, attributes[prop]);
    }

    return el;
  }

  private createContainer(): void {
    const exist = document.getElementById("notifier-container");

    if (exist) {
      return;
    }

    const container = this.createElement("div", { class: "notifier-container", id: "notifier-container" });
    document.body.appendChild(container);
  }
}
