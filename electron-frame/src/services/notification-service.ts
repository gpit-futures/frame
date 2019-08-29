import axios from "axios";
import { SearchService } from "../services/search-service";

export class NotificationService {
  constructor() {
    this.frameworkBackendUrl = "http://localhost:8080"
  }

  private notificationList: INotifications[];
  private frameworkBackendUrl : string;

  async getNotifications(token: string): Promise<INotifications[]> {
    this.notificationList = await axios.get(`${this.frameworkBackendUrl}/api/notifications`,
      {
        headers: {
          "Authorization": 'Bearer ' + token
        }
      })
      .then(response => response.data);

    let searchService = new SearchService();
    for (var i = 0; i < this.notificationList.length; i++) {
      let tempPatient = await searchService.getPatient(token, this.notificationList[i].nhsNumber)
      this.notificationList[i].patientName = tempPatient.name[0].family +
        ", " +
        tempPatient.name[0].given[0] +
        " (" +
        tempPatient.name[0].prefix[0] +
        ")"
    }

    return this.notificationList;
  }

  async removeNotification(token: string, id: string) {
    await axios.post(`${this.frameworkBackendUrl}/api/notifications/${id}/read`, null,
      {
        headers: {
          "Authorization": 'Bearer ' + token
        }
      })
      .then(response => response.data);
  }
}

export interface INotifications {
  id: string;
  ods: string;
  nhsNumber: string;
  read: string[];
  summary: string;
  details: string;
  type: string;
  dateCreated: string;
  patientName: string;
}
