internal static class Config
{
    public static class Terminal
    {
        public const string Font     = "/usr/share/fonts/TTF/DejaVuSansMono.ttf";
        public const float  FontSize = 20f;

        public const float PaddingX = 8f;
        public const float PaddingY = 6f;

        public const int MaxScrollback = 5000;
    }

    public static class Colors
    {
        public const int Foreground    = 0xDCDCDC;
        public const int Background    = 0x0A0A00;
        public const int Cursor        = 0xD8DEE9;
        public const int Black         = 0x000000;
        public const int Red           = 0xD63B67;
        public const int Green         = 0x08D14B;
        public const int Yellow        = 0xF6B612;
        public const int Blue          = 0x21AFDD;
        public const int Magenta       = 0xA348C7;
        public const int Cyan          = 0x0CE4D1;
        public const int White         = 0xF7F7F7;
        public const int BrightBlack   = 0x6E6E6E;
        public const int BrightRed     = 0xF85E89;
        public const int BrightGreen   = 0x26E161;
        public const int BrightYellow  = 0xFBCD55;
        public const int BrightBlue    = 0x4AC8F1;
        public const int BrightMagenta = 0xC481DF;
        public const int BrightCyan    = 0x35E3D5;
        public const int BrightWhite   = 0xFFFFFF;
    }
}