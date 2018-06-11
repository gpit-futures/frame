import axios from "axios";

export class UserService {
  constructor() { }

  private userList: IUser[];

  async getUsers(): Promise<IUser[]> {
    if (!this.userList) {
      const gist = await axios.get("https://api.github.com/gists/808ce2353c0ca20e85968d33d9c76944?client_id=0b5c1b8619519671add3&client_secret=8d460cba9c0febc51eabf627d9936fbb654892a1")
      .then(response => response.data);

      const rawUrl = gist.files["user-list.json"].raw_url;

      this.userList = await axios.get(rawUrl + '?client_id=0b5c1b8619519671add3&client_secret=8d460cba9c0febc51eabf627d9936fbb654892a1')
      .then(response => response.data);
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
