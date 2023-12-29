namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ModApi.Common;
    using ModApi.Settings.Core;

    /// <summary>
    /// The settings for the mod.
    /// </summary>
    /// <seealso cref="ModApi.Settings.Core.SettingsCategory{Assets.Scripts.ModSettings}" />
    public class ModSettings : SettingsCategory<ModSettings>
    {
        /// <summary>
        /// The mod settings instance.
        /// </summary>
        private static ModSettings _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModSettings"/> class.
        /// </summary>
        public ModSettings() : base("Rumbler")
        {
        }

        /// <summary>
        /// Gets the mod settings instance.
        /// </summary>
        /// <value>
        /// The mod settings instance.
        /// </value>
        public static ModSettings Instance => _instance ?? (_instance = Game.Instance.Settings.ModSettings.GetCategory<ModSettings>());

        ///// <summary>
        ///// Gets the TestSetting1 value
        ///// </summary>
        ///// <value>
        ///// The TestSetting1 value.
        ///// </value>
        //public NumericSetting<float> TestSetting1 { get; private set; }
        public NumericSetting<float> RumbleIntensity { get; private set; }

        public NumericSetting<float> IntensityMax { get; private set; }

        public BoolSetting explosionRumble { get; private set; }
        public BoolSetting stagingRumble { get; private set; }
        public BoolSetting accelRumble { get; private set; }
        public NumericSetting<float> accelStrength { get; private set; }

        public NumericSetting<int> accelMotor { get; private set; }
        public NumericSetting<int> shockMotor { get; private set; }

        /// <summary>
        /// Initializes the settings in the category.
        /// </summary>
        protected override void InitializeSettings()
        {
            //this.TestSetting1 = this.CreateNumeric<float>("Test Setting 1", 1f, 10f, 1f)
            //    .SetDescription("A test setting that does nothing.")
            //    .SetDisplayFormatter(x => x.ToString("F1"))
            //    .SetDefault(2f);

            this.RumbleIntensity = this.CreateNumeric<float>("Rumble Intensity", 0f, 3f, 0.1f)
                .SetDescription("Intensity of controller vibrations")
                .SetDefault(1f);

            this.IntensityMax = this.CreateNumeric<float>("Maximum Intensity", 0f, 2f, 0.1f)
                .SetDescription("Rumble intensity that won't be exceeded")
                .SetDefault(1f);

            this.explosionRumble = this.CreateBool("Explosion Rumble")
                .SetDescription("Explosions will cause rumble")
                .SetDefault(true);

            this.stagingRumble = this.CreateBool("Staging Rumble")
                .SetDescription("Activating stages will cause rumble")
                .SetDefault(true);

            this.accelRumble = this.CreateBool("Acceleration Rumble")
                .SetDescription("Acceleration will cause rumble")
                .SetDefault(true);

            this.accelStrength = this.CreateNumeric<float>("Acceleration Strength Multiplier", 0.2f, 2f, 0.1f)
                .SetDescription("Strength of acceleration rumble")
                .SetDefault(1f);

            this.accelMotor = this.CreateNumeric<int>("Acceleration Motor Index", 0, 3, 1)
                .SetDescription("Motor to be used for acceleration rumble. IMPORTANT: Make sure shock and acceleration aren't using the same motor.")
                .SetDefault(1);

            this.shockMotor = this.CreateNumeric<int>("Shock Motor Index", 0, 3, 1)
                .SetDescription("Motor to be used for shock rumble (explosions and stage separations). IMPORTANT: Make sure shock and acceleration aren't using the same motor.")
                .SetDefault(0);

        }
    }
}