import { FrameworkConfiguration } from "aurelia-framework";
import { PLATFORM } from "aurelia-pal";

export function configure(config: FrameworkConfiguration) {
  config.globalResources([
    PLATFORM.moduleName("./elements/sidebar"),
    PLATFORM.moduleName("./elements/sidebar-icon.html"),
    PLATFORM.moduleName("./elements/info-bar"),
    PLATFORM.moduleName("./elements/patient-banner"),
    PLATFORM.moduleName("./elements/patient-banner-supplement"),
    PLATFORM.moduleName("./elements/frames"),

    PLATFORM.moduleName("./value-converters/date-time-format"),
    PLATFORM.moduleName("./value-converters/age-today"),
    PLATFORM.moduleName("./value-converters/nhs-number-format")
  ]);
}
