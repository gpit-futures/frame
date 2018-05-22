import { autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";

@autoinject
export class UserService {
  constructor(private http: HttpClient) { }

  private userList: IUser[];

  async getUsers(): Promise<IUser[]> {
    if (!this.userList) {
      const gist = await this.http
        .fetch("https://api.github.com/gists/808ce2353c0ca20e85968d33d9c76944")
        .then(response => response.json());

      const rawUrl = gist.files["user-list.json"].raw_url;

      this.userList = await this.http
        .fetch(rawUrl)
        .then(response => response.json());
    }

    return this.userList;
  }

  async getUser(id: string): Promise<IUser> {
    await this.getUsers();

    return this.userList.find(x => x.id === id);
  }
}

export interface IUser {
  id: string;
  username: string;
  title: string;
  firstName: string;
  lastName: string;
}
