import * as moment from "moment";

export class AgeTodayValueConverter {
  toView(dob, template = "(%v)") {
    const today = moment();
    const date = moment(dob);

    const months = today.diff(date, "months");
    let result = "";

    if (months > 24) {
      result = `${today.diff(date, "years")}y`;
    } else {
      // todo: when to show age in weeks/days?
      result = `${months}m`;
    }

    return template.replace("%v", result);
  }
}
