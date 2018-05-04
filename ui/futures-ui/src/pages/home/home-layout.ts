import { observable } from "aurelia-framework";

import { Catalogue } from "../../fake-app-catalogue";

export class HomeLayout {
  selected: string;

  @observable clients: Catalogue.IClientMetadata[] = [];
  @observable patientContext: boolean;

  patient = {
    "id": "267e175a-57fe-4b8a-a672-15012d83ed9e",
    "title": "Mr",
    "firstName": "Ivor",
    "lastName": "Cox",
    "gender": "Male",
    "phone": "(011981) 32362",
    "pasNumber": "352541",
    "nhsNumber": "9999999000",
    "dateOfBirth": "1944-07-05T23:00:00Z",
    "address": {
      "line1": "6948 Et St.",
      "line2": "Halesowen",
      "line3": "Worcestershire",
      "line4": null,
      "postcode": "VX27 5DV"
    },
    "gp": {
      "name": "Goff Carolyn D.",
      "address": {
        "line1": "Hamilton Practice",
        "line2": "5544 Ante Street",
        "line3": "Hamilton",
        "line4": "Lanarkshire",
        "postcode": "N06 5LP"
      }
    }
  };

  async attached() {
    this.clients = await Catalogue.getClients();

    this.selected = this.clients[0].id;

    window.setTimeout(() => {
      this.patientContext = !this.patientContext;
    }, 4000);
  }
}
