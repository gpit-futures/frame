import * as moment from "moment";

export class DateTimeFormatValueConverter {
  toView(date, format: string = "dddd MMM DD YYYY | HH:mm") {
    return moment(date).format(format);
  }
}
