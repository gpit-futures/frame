export class NhsNumberFormatValueConverter {
  toView(nhsNumber = "") {
    const tidy = nhsNumber.trim();

    if(tidy.length < 10) {
      return "UNKNOWN";
    }

    return `${tidy.substr(0, 3)} ${tidy.substr(3, 3)} ${tidy.substr(6)}`;
  }
}
