import { FrameworkConfiguration } from "aurelia-framework";
import { PLATFORM } from "aurelia-pal";

export function configure(config: FrameworkConfiguration) {
  config.globalResources([
    PLATFORM.moduleName("./elements/sidebar/sidebar"),
    PLATFORM.moduleName("./elements/sidebar/sidebar-icon.html"),
    PLATFORM.moduleName("./elements/sidebar/info-bar"),
    PLATFORM.moduleName("./elements/sidebar/patient-banner"),
    PLATFORM.moduleName("./elements/sidebar/patient-banner-supplement"),
    PLATFORM.moduleName("./elements/frames/frames"),

    PLATFORM.moduleName("./value-converters/date-time-format"),
    PLATFORM.moduleName("./value-converters/age-today"),
    PLATFORM.moduleName("./value-converters/nhs-number-format")
  ]);
}
