﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer.Decoder
{
    public class ExifManager : IDisposable
    {

        private readonly System.Drawing.Bitmap _image;
        private Encoding _encoding = Encoding.UTF8;

        // Type declarations 

        #region Type declarations

        /// <summary>
        /// Contains possible values of EXIF tag names (ID)
        /// </summary>
        public enum TagNames : int
        {
            ExifIFD = 0x8769,
            GpsIFD = 0x8825,
            NewSubfileType = 0xFE,
            SubfileType = 0xFF,
            ImageWidth = 0x100,
            ImageHeight = 0x101,
            BitsPerSample = 0x102,
            Compression = 0x103,
            PhotometricInterp = 0x106,
            ThreshHolding = 0x107,
            CellWidth = 0x108,
            CellHeight = 0x109,
            FillOrder = 0x10A,
            DocumentName = 0x10D,
            ImageDescription = 0x10E,
            EquipMake = 0x10F,
            EquipModel = 0x110,
            StripOffsets = 0x111,
            Orientation = 0x112,
            SamplesPerPixel = 0x115,
            RowsPerStrip = 0x116,
            StripBytesCount = 0x117,
            MinSampleValue = 0x118,
            MaxSampleValue = 0x119,
            XResolution = 0x11A,
            YResolution = 0x11B,
            PlanarConfig = 0x11C,
            PageName = 0x11D,
            XPosition = 0x11E,
            YPosition = 0x11F,
            FreeOffset = 0x120,
            FreeByteCounts = 0x121,
            GrayResponseUnit = 0x122,
            GrayResponseCurve = 0x123,
            T4Option = 0x124,
            T6Option = 0x125,
            ResolutionUnit = 0x128,
            PageNumber = 0x129,
            TransferFuncition = 0x12D,
            SoftwareUsed = 0x131,
            DateTime = 0x132,
            Artist = 0x13B,
            HostComputer = 0x13C,
            Predictor = 0x13D,
            WhitePoint = 0x13E,
            PrimaryChromaticities = 0x13F,
            ColorMap = 0x140,
            HalftoneHints = 0x141,
            TileWidth = 0x142,
            TileLength = 0x143,
            TileOffset = 0x144,
            TileByteCounts = 0x145,
            InkSet = 0x14C,
            InkNames = 0x14D,
            NumberOfInks = 0x14E,
            DotRange = 0x150,
            TargetPrinter = 0x151,
            ExtraSamples = 0x152,
            SampleFormat = 0x153,
            SMinSampleValue = 0x154,
            SMaxSampleValue = 0x155,
            TransferRange = 0x156,
            JPEGProc = 0x200,
            JPEGInterFormat = 0x201,
            JPEGInterLength = 0x202,
            JPEGRestartInterval = 0x203,
            JPEGLosslessPredictors = 0x205,
            JPEGPointTransforms = 0x206,
            JPEGQTables = 0x207,
            JPEGDCTables = 0x208,
            JPEGACTables = 0x209,
            YCbCrCoefficients = 0x211,
            YCbCrSubsampling = 0x212,
            YCbCrPositioning = 0x213,
            REFBlackWhite = 0x214,
            ICCProfile = 0x8773,
            Gamma = 0x301,
            ICCProfileDescriptor = 0x302,
            SRGBRenderingIntent = 0x303,
            ImageTitle = 0x320,
            Copyright = 0x8298,
            ResolutionXUnit = 0x5001,
            ResolutionYUnit = 0x5002,
            ResolutionXLengthUnit = 0x5003,
            ResolutionYLengthUnit = 0x5004,
            PrintFlags = 0x5005,
            PrintFlagsVersion = 0x5006,
            PrintFlagsCrop = 0x5007,
            PrintFlagsBleedWidth = 0x5008,
            PrintFlagsBleedWidthScale = 0x5009,
            HalftoneLPI = 0x500A,
            HalftoneLPIUnit = 0x500B,
            HalftoneDegree = 0x500C,
            HalftoneShape = 0x500D,
            HalftoneMisc = 0x500E,
            HalftoneScreen = 0x500F,
            JPEGQuality = 0x5010,
            GridSize = 0x5011,
            ThumbnailFormat = 0x5012,
            ThumbnailWidth = 0x5013,
            ThumbnailHeight = 0x5014,
            ThumbnailColorDepth = 0x5015,
            ThumbnailPlanes = 0x5016,
            ThumbnailRawBytes = 0x5017,
            ThumbnailSize = 0x5018,
            ThumbnailCompressedSize = 0x5019,
            ColorTransferFunction = 0x501A,
            ThumbnailData = 0x501B,
            ThumbnailImageWidth = 0x5020,
            ThumbnailImageHeight = 0x502,
            ThumbnailBitsPerSample = 0x5022,
            ThumbnailCompression = 0x5023,
            ThumbnailPhotometricInterp = 0x5024,
            ThumbnailImageDescription = 0x5025,
            ThumbnailEquipMake = 0x5026,
            ThumbnailEquipModel = 0x5027,
            ThumbnailStripOffsets = 0x5028,
            ThumbnailOrientation = 0x5029,
            ThumbnailSamplesPerPixel = 0x502A,
            ThumbnailRowsPerStrip = 0x502B,
            ThumbnailStripBytesCount = 0x502C,
            ThumbnailResolutionX = 0x502D,
            ThumbnailResolutionY = 0x502E,
            ThumbnailPlanarConfig = 0x502F,
            ThumbnailResolutionUnit = 0x5030,
            ThumbnailTransferFunction = 0x5031,
            ThumbnailSoftwareUsed = 0x5032,
            ThumbnailDateTime = 0x5033,
            ThumbnailArtist = 0x5034,
            ThumbnailWhitePoint = 0x5035,
            ThumbnailPrimaryChromaticities = 0x5036,
            ThumbnailYCbCrCoefficients = 0x5037,
            ThumbnailYCbCrSubsampling = 0x5038,
            ThumbnailYCbCrPositioning = 0x5039,
            ThumbnailRefBlackWhite = 0x503A,
            ThumbnailCopyRight = 0x503B,
            LuminanceTable = 0x5090,
            ChrominanceTable = 0x5091,
            FrameDelay = 0x5100,
            LoopCount = 0x5101,
            PixelUnit = 0x5110,
            PixelPerUnitX = 0x5111,
            PixelPerUnitY = 0x5112,
            PaletteHistogram = 0x5113,
            ExifExposureTime = 0x829A,
            ExifFNumber = 0x829D,
            ExifExposureProg = 0x8822,
            ExifSpectralSense = 0x8824,
            ExifISOSpeed = 0x8827,
            ExifOECF = 0x8828,
            ExifVer = 0x9000,
            ExifDTOrig = 0x9003,
            ExifDTDigitized = 0x9004,
            ExifCompConfig = 0x9101,
            ExifCompBPP = 0x9102,
            ExifShutterSpeed = 0x9201,
            ExifAperture = 0x9202,
            ExifBrightness = 0x9203,
            ExifExposureBias = 0x9204,
            ExifMaxAperture = 0x9205,
            ExifSubjectDist = 0x9206,
            ExifMeteringMode = 0x9207,
            ExifLightSource = 0x9208,
            ExifFlash = 0x9209,
            ExifFocalLength = 0x920A,
            ExifMakerNote = 0x927C,
            ExifUserComment = 0x9286,
            ExifDTSubsec = 0x9290,
            ExifDTOrigSS = 0x9291,
            ExifDTDigSS = 0x9292,
            ExifFPXVer = 0xA000,
            ExifColorSpace = 0xA001,
            ExifPixXDim = 0xA002,
            ExifPixYDim = 0xA003,
            ExifRelatedWav = 0xA004,
            ExifInterop = 0xA005,
            ExifFlashEnergy = 0xA20B,
            ExifSpatialFR = 0xA20C,
            ExifFocalXRes = 0xA20E,
            ExifFocalYRes = 0xA20F,
            ExifFocalResUnit = 0xA210,
            ExifSubjectLoc = 0xA214,
            ExifExposureIndex = 0xA215,
            ExifSensingMethod = 0xA217,
            ExifFileSource = 0xA300,
            ExifSceneType = 0xA301,
            ExifCfaPattern = 0xA302,
            GpsVer = 0x0,
            GpsLatitudeRef = 0x1,
            GpsLatitude = 0x2,
            GpsLongitudeRef = 0x3,
            GpsLongitude = 0x4,
            GpsAltitudeRef = 0x5,
            GpsAltitude = 0x6,
            GpsGpsTime = 0x7,
            GpsGpsSatellites = 0x8,
            GpsGpsStatus = 0x9,
            GpsGpsMeasureMode = 0xA,
            GpsGpsDop = 0xB,
            GpsSpeedRef = 0xC,
            GpsSpeed = 0xD,
            GpsTrackRef = 0xE,
            GpsTrack = 0xF,
            GpsImgDirRef = 0x10,
            GpsImgDir = 0x11,
            GpsMapDatum = 0x12,
            GpsDestLatRef = 0x13,
            GpsDestLat = 0x14,
            GpsDestLongRef = 0x15,
            GpsDestLong = 0x16,
            GpsDestBearRef = 0x17,
            GpsDestBear = 0x18,
            GpsDestDistRef = 0x19,
            GpsDestDist = 0x1A
        }


        /// <summary>
        /// Real position of 0th row and column of picture
        /// </summary>
        public enum Orientations
        {
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3,
            BottomLeft = 4,
            LeftTop = 5,
            RightTop = 6,
            RightBottom = 7,
            LftBottom = 8
        }


        /// <summary>
        /// Exposure programs
        /// </summary>
        public enum ExposurePrograms
        {
            Manual = 1,
            Normal = 2,
            AperturePriority = 3,
            ShutterPriority = 4,
            Creative = 5,
            Action = 6,
            Portrait = 7,
            Landscape = 8,
        }


        /// <summary>
        /// Exposure metering modes
        /// </summary>
        public enum ExposureMeteringModes
        {
            Unknown = 0,
            Average = 1,
            CenterWeightedAverage = 2,
            Spot = 3,
            MultiSpot = 4,
            MultiSegment = 5,
            Partial = 6,
            Other = 255
        }


        /// <summary>
        /// Flash activity modes
        /// </summary>
        public enum FlashModes
        {
            NotFired = 0,
            Fired = 1,
            FiredButNoStrobeReturned = 5,
            FiredAndStrobeReturned = 7,
        }


        /// <summary>
        /// Possible light sources (white balance)
        /// </summary>
        public enum LightSources
        {
            Unknown = 0,
            Daylight = 1,
            Fluorescent = 2,
            Tungsten = 3,
            Flash = 10,
            StandardLightA = 17,
            StandardLightB = 18,
            StandardLightC = 19,
            D55 = 20,
            D65 = 21,
            D75 = 22,
            Other = 255
        }


        // 
        // EXIF data types
        // 
        // 
        // 
        // [altair] 12.6.2004 Created
        // 
        public enum ExifDataTypes : short
        {
            UnsignedByte = 1,
            AsciiString = 2,
            UnsignedShort = 3,
            UnsignedLong = 4,
            UnsignedRational = 5,
            SignedByte = 6,
            Undefined = 7,
            SignedShort = 8,
            SignedLong = 9,
            SignedRational = 10,
            SingleFloat = 11,
            DoubleFloat = 12
        }


        // 
        // Represents rational which is type of some Exif properties
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public struct Rational
        {
            public int Numerator;
            public int Denominator;


            // 
            // Converts rational to string representation
            // 
            // Optional, default "/". String to be used as delimiter of components.
            // String representation of the rational.
            // 
            // 
            // [altair] 10.09.2003 Created
            // 

            public override string ToString()
            {
                return ToString("/");
            }

            public string ToString(string Delimiter)
            {
                return Numerator + "/" + Denominator;
            }

            // 
            // Converts rational to double precision real number
            // 
            // The rational as double precision real number.
            // 
            // 
            // [altair] 10.09.2003 Created
            // 

            public double ToDouble()
            {
                return (double) Numerator/Denominator;
            }
        }

        #endregion

        // 
        // Initializes new instance of this class.
        // 
        // Bitmap to read exif information from
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public ExifManager(System.Drawing.Bitmap Bitmap)
        {
            if (Bitmap == null)
                throw new ArgumentNullException("Bitmap");
            _image = Bitmap;
        }

        // 
        // Initializes new instance of this class.
        // 
        // Name of file to be loaded
        // 
        // 
        // [altair] 13.06.2004 Created
        // 
        public ExifManager(string FileName)
        {
            _image = (System.Drawing.Bitmap) System.Drawing.Image.FromFile(FileName);
        }

        // 
        // Get or set encoding used for string metadata
        // 
        // Encoding used for string metadata
        // Default encoding is UTF-8
        // 
        // [altair] 11.07.2004 Created
        // [altair] 05.09.2005 Changed from shared to instance member
        // 
        public Encoding Encoding
        {
            get { return _encoding; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _encoding = value;
            }
        }

        // 
        // Returns copy of bitmap this instance is working on
        // 
        // 
        // 
        // 
        // [altair] 13.06.2004 Created
        // 
        public System.Drawing.Bitmap GetBitmap()
        {
            return (System.Drawing.Bitmap) _image.Clone();
        }

        // 
        // Returns all available data in formatted string form
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public override string ToString()
        {
            var SB = new StringBuilder();

            SB.Append("Image:");
            SB.Append("\n\tDimensions: " + Width + " x " + Height + " px");
            SB.Append("\n\tResolution: " + ResolutionX + " x " + ResolutionY + " dpi");
            SB.Append("\n\tOrientation: " + Enum.GetName(typeof (Orientations), Orientation));
            SB.Append("\n\tTitle: " + Title);
            SB.Append("\n\tDescription: " + Description);
            SB.Append("\n\tCopyright: " + Copyright);
            SB.Append("\nEquipment:");
            SB.Append("\n\tMaker: " + EquipmentMaker);
            SB.Append("\n\tModel: " + EquipmentModel);
            SB.Append("\n\tSoftware: " + Software);
            SB.Append("\nDate and time:");
            SB.Append("\n\tGeneral: " + DateTimeLastModified.ToString());
            SB.Append("\n\tOriginal: " + DateTimeOriginal.ToString());
            SB.Append("\n\tDigitized: " + DateTimeDigitized.ToString());
            SB.Append("\nShooting conditions:");
            SB.Append("\n\tExposure time: " + ExposureTime.ToString("N4") + " s");
            SB.Append("\n\tExposure program: " + Enum.GetName(typeof (ExposurePrograms), ExposureProgram));
            SB.Append("\n\tExposure mode: " + Enum.GetName(typeof (ExposureMeteringModes), ExposureMeteringMode));
            SB.Append("\n\tAperture: F" + Aperture.ToString("N2"));
            SB.Append("\n\tISO sensitivity: " + ISO);
            SB.Append("\n\tSubject distance: " + SubjectDistance.ToString("N2") + " m");
            SB.Append("\n\tFocal length: " + FocalLength);
            SB.Append("\n\tFlash: " + Enum.GetName(typeof (FlashModes), FlashMode));
            SB.Append("\n\tLight source (WB): " + Enum.GetName(typeof (LightSources), LightSource));
            //SB.Replace("\n", vbCrLf);
            //SB.Replace("\t", vbTab);
            return SB.ToString();
        }

        //Nicely formatted 
        //well 
        //known properties 

        #region Nicely formatted well-known properties

        /// <summary>
        /// Brand of equipment (EXIF EquipMake)
        /// </summary>
        public string EquipmentMaker => GetPropertyString((int) TagNames.EquipMake);

        /// <summary>
        /// Model of equipment (EXIF EquipModel)
        /// </summary>
        public string EquipmentModel => GetPropertyString((int) TagNames.EquipModel);

        /// <summary>
        /// Software used for processing (EXIF Software)
        /// </summary>
        public string Software => GetPropertyString((int) TagNames.SoftwareUsed);

        /// <summary>
        /// Orientation of image (position of row 0, column 0) (EXIF Orientation)
        /// </summary>
        public Orientations Orientation
        {
            get
            {
                int X = GetPropertyInt16((int) TagNames.Orientation);

                if (!Enum.IsDefined(typeof (Orientations), X))
                    return Orientations.TopLeft;
                else
                    return (Orientations) Enum.Parse(typeof (Orientations), Enum.GetName(typeof (Orientations), X));
            }
        }

        /// <summary>
        /// Time when image was last modified (EXIF DateTime).
        /// </summary>
        public DateTime DateTimeLastModified
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(GetPropertyString((int) TagNames.DateTime),
                        @"yyyy\:MM\:dd HH\:mm\:ss", null);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.DateTime, value.ToString(@"yyyy\:MM\:dd HH\:mm\:ss"));
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Time when image was taken (EXIF DateTimeOriginal).
        /// </summary>
        public DateTime DateTimeOriginal
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(GetPropertyString((int) TagNames.ExifDTOrig),
                        @"yyyy\:MM\:dd HH\:mm\:ss", null);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.ExifDTOrig, value.ToString(@"yyyy\:MM\:dd HH\:mm\:ss"));
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Time when image was digitized (EXIF DateTimeDigitized).
        /// </summary>
        public DateTime DateTimeDigitized
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(GetPropertyString((int) TagNames.ExifDTDigitized),
                        @"yyyy\:MM\:dd HH\:mm\:ss", null);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.ExifDTDigitized, value.ToString(@"yyyy\:MM\:dd HH\:mm\:ss"));
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Image width
        /// </summary>
        public int Width => _image.Width;

        // 
        // Image height
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // [altair] 04.09.2005 Changed output to Int32, load from image instead of EXIF
        // 
        public int Height => _image.Height;

        // 
        // X resolution in dpi (EXIF XResolution/ResolutionUnit)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public double ResolutionX
        {
            get
            {
                var R = GetPropertyRational((int) TagNames.XResolution).ToDouble();

                if (GetPropertyInt16((int) TagNames.ResolutionUnit) == 3)
                {
                    // -- resolution is in points/cm
                    return R*2.54;
                }
                else
                {
                    // -- resolution is in points/inch
                    return R;
                }
            }
        }

        // 
        // Y resolution in dpi (EXIF YResolution/ResolutionUnit)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public double ResolutionY
        {
            get
            {
                var R = GetPropertyRational((int) TagNames.YResolution).ToDouble();

                if (GetPropertyInt16((int) TagNames.ResolutionUnit) == 3)
                {
                    // -- resolution is in points/cm
                    return R*2.54;
                }
                else
                {
                    // -- resolution is in points/inch
                    return R;
                }
            }
        }

        // 
        // Image title (EXIF ImageTitle)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public string Title
        {
            get { return GetPropertyString((int) TagNames.ImageTitle); }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.ImageTitle, value);
                }
                catch
                {
                }
            }
        }

        // 
        // User comment (EXIF UserComment)
        // 
        // 
        // 
        // 
        // [altair] 13.06.2004 Created
        // 
        public string UserComment
        {
            get { return GetPropertyString((int) TagNames.ExifUserComment); }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.ExifUserComment, value);
                }
                catch
                {
                }
            }
        }

        // 
        // Artist name (EXIF Artist)
        // 
        // 
        // 
        // 
        // [altair] 13.06.2004 Created
        // 
        public string Artist
        {
            get { return GetPropertyString((int) TagNames.Artist); }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.Artist, value);
                }
                catch
                {
                }
            }
        }

        // 
        // Image description (EXIF ImageDescription)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public string Description
        {
            get { return GetPropertyString((int) TagNames.ImageDescription); }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.ImageDescription, value);
                }
                catch
                {
                }
            }
        }

        // 
        // Image copyright (EXIF Copyright)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public string Copyright
        {
            get { return GetPropertyString((int) TagNames.Copyright); }
            set
            {
                try
                {
                    SetPropertyString((int) TagNames.Copyright, value);
                }
                catch
                {
                }
            }
        }


        // 
        // Exposure time in seconds (EXIF ExifExposureTime/ExifShutterSpeed)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public double ExposureTimeAbs
        {
            get
            {
                if (IsPropertyDefined((int) TagNames.ExifExposureTime))
                    // -- Exposure time is explicitly specified
                    return GetPropertyRational((int) TagNames.ExifExposureTime).ToDouble();
                else if (IsPropertyDefined((int) TagNames.ExifShutterSpeed))
                    //'-- Compute exposure time from shutter spee 
                    return (1/Math.Pow(2, GetPropertyRational((int) TagNames.ExifShutterSpeed).ToDouble()));
                else
                // -- Can't figure out 
                    return 0;
            }
        }

        public Rational ExposureTime
        {
            get
            {
                if (IsPropertyDefined((int) TagNames.ExifExposureTime))
                    // -- Exposure time is explicitly specified
                    return GetPropertyRational((int) TagNames.ExifExposureTime);
                else
                    return new Rational();
            }
        }

        // 
        // Aperture value as F number (EXIF ExifFNumber/ExifApertureValue)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public double Aperture
        {
            get
            {
                if (IsPropertyDefined((int) TagNames.ExifFNumber))
                    return GetPropertyRational((int) TagNames.ExifFNumber).ToDouble();
                else if (IsPropertyDefined((int) TagNames.ExifAperture))
                    return Math.Pow(Math.Sqrt(2),
                        GetPropertyRational((int) TagNames.ExifAperture).ToDouble());
                else
                    return 0;
            }
        }

        // 
        // Exposure program used (EXIF ExifExposureProg)
        // 
        // 
        // If not specified, returns Normal (2)
        // 
        // [altair] 10.09.2003 Created
        // 
        public ExposurePrograms ExposureProgram
        {
            get
            {
                int X = GetPropertyInt16((int) TagNames.ExifExposureProg);

                if (Enum.IsDefined(typeof (ExposurePrograms), X))
                    return
                        (ExposurePrograms)
                            Enum.Parse(typeof (ExposurePrograms), Enum.GetName(typeof (ExposurePrograms), X));
                else
                    return ExposurePrograms.Normal;
            }
        }

        // 
        // ISO sensitivity
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public short ISO => GetPropertyInt16((int) TagNames.ExifISOSpeed);

        // 
        // Subject distance in meters (EXIF SubjectDistance)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public double SubjectDistance => GetPropertyRational((int) TagNames.ExifSubjectDist).ToDouble();

        // 
        // Exposure method metering mode used (EXIF MeteringMode)
        // 
        // 
        // If not specified, returns Unknown (0)
        // 
        // [altair] 10.09.2003 Created
        // 
        public ExposureMeteringModes ExposureMeteringMode
        {
            get
            {
                int X = GetPropertyInt16((int) TagNames.ExifMeteringMode);

                if (Enum.IsDefined(typeof (ExposureMeteringModes), X))
                    return
                        (ExposureMeteringModes)
                            Enum.Parse(typeof (ExposureMeteringModes), Enum.GetName(typeof (ExposureMeteringModes), X));
                else
                    return ExposureMeteringModes.Unknown;
            }
        }

        // 
        // Focal length of lenses in mm (EXIF FocalLength)
        // 
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public double FocalLength => GetPropertyRational((int) TagNames.ExifFocalLength).ToDouble();

        // 
        // Flash mode (EXIF Flash)
        // 
        // 
        // If not present, value NotFired (0) is returned
        // 
        // [altair] 10.09.2003 Created
        // 
        public FlashModes FlashMode
        {
            get
            {
                int X = GetPropertyInt16((int) TagNames.ExifFlash);

                if (Enum.IsDefined(typeof (FlashModes), X))
                    return (FlashModes) Enum.Parse(typeof (FlashModes), Enum.GetName(typeof (FlashModes), X));
                else
                    return FlashModes.NotFired;
            }
        }

        // 
        // Light source / white balance (EXIF LightSource)
        // 
        // 
        // If not specified, returns Unknown (0).
        // 
        // [altair] 10.09.2003 Created
        // 
        public LightSources LightSource
        {
            get
            {
                int X = GetPropertyInt16((int) TagNames.ExifLightSource);

                if (Enum.IsDefined(typeof (LightSources), X))
                    return (LightSources) Enum.Parse(typeof (LightSources), Enum.GetName(typeof (LightSources), X));
                else
                    return LightSources.Unknown;
            }
        }

        #endregion

        //Support methods 
        //for
        //working with 
        //EXIF properties 

        #region Support methods for working with EXIF properties

// 
// Checks if current image has specified certain property
// 
// 
// True if image has specified property, False otherwise.
// 
// 
// [altair] 10.09.2003 Created
// 
        public bool IsPropertyDefined(int PID)
        {
            return (Array.IndexOf(_image.PropertyIdList, PID) > -1);
        }

        // 
        // Gets specified Int32 property
        // 
        // Property ID
        // Optional, default 0. Default value returned if property is not present.
        // Value of property or DefaultValue if property is not present.
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public int GetPropertyInt32(int PID)
        {
            return GetPropertyInt32(PID, 0);
        }

        public int GetPropertyInt32(int PID, int DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return GetInt32(_image.GetPropertyItem(PID).Value);
            else
                return DefaultValue;
        }

        // 
        // Gets specified Int16 property
        // 
        // Property ID
        // Optional, default 0. Default value returned if property is not present.
        // Value of property or DefaultValue if property is not present.
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public short GetPropertyInt16(int PID)
        {
            return GetPropertyInt16(PID, 0);
        }

        public short GetPropertyInt16(int PID, short DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return GetInt16(_image.GetPropertyItem(PID).Value);
            else
                return DefaultValue;
        }

        // 
        // Gets specified string property
        // 
        // Property ID
        // Optional, default String.Empty. Default value returned if property is not present.
        // 
        // Value of property or DefaultValue if property is not present.
        // 
        // [altair] 10.09.2003 Created
        // 
        public string GetPropertyString(int PID)
        {
            return GetPropertyString(PID, "");
        }

        public string GetPropertyString(int PID, string DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return GetString(_image.GetPropertyItem(PID).Value);
            else
                return DefaultValue;
        }

        // 
        // Gets specified property in raw form
        // 
        // Property ID
        // Optional, default Nothing. Default value returned if property is not present.
        // 
        // Is recommended to use typed methods (like etc.) instead, when possible.
        // 
        // [altair] 05.09.2005 Created
        // 
        public byte[] GetProperty(int PID, byte[] DefaultValue)
        {
            if (IsPropertyDefined(PID))
                return _image.GetPropertyItem(PID).Value;
            else
                return DefaultValue;
        }

        public byte[] GetProperty(int PID)
        {
            return GetProperty(PID, null);
        }

        // 
        // Gets specified rational property
        // 
        // Property ID
        // 
        // Value of property or 0/1 if not present.
        // 
        // [altair] 10.09.2003 Created
        // 
        public Rational GetPropertyRational(int PID)
        {
            if (IsPropertyDefined(PID))
                return GetRational(_image.GetPropertyItem(PID).Value);
            else
            {
                Rational R;
                R.Numerator = 0;
                R.Denominator = 1;
                return R;
            }
        }

        // 
        // Sets specified string property
        // 
        // Property ID
        // Value to be set
        // 
        // 
        // [altair] 12.6.2004 Created
        // 
        public void SetPropertyString(int PID, string Value)
        {
            var Data = _encoding.GetBytes(Value + '\0');
            SetProperty(PID, Data, ExifDataTypes.AsciiString);
        }

        // 
        // Sets specified Int16 property
        // 
        // Property ID
        // Value to be set
        // 
        // 
        // [altair] 12.6.2004 Created
        // 
        public void SetPropertyInt16(int PID, short Value)
        {
            var Data = new byte[2];
            Data[0] = (byte) (Value & 0xFF);
            Data[1] = (byte) ((Value & 0xFF00) >> 8);
            SetProperty(PID, Data, ExifDataTypes.SignedShort);
        }

        // 
        // Sets specified Int32 property
        // 
        // Property ID
        // Value to be set
        // 
        // 
        // [altair] 13.06.2004 Created
        // 
        public void SetPropertyInt32(int PID, int Value)
        {
            var Data = new byte[4];
            for (var I = 0; I < 4; I++)
            {
                Data[I] = (byte) (Value & 0xFF);
                Value >>= 8;
            }
            SetProperty(PID, Data, ExifDataTypes.SignedLong);
        }

        // 
        // Sets specified property in raw form
        // 
        // Property ID
        // Raw data
        // EXIF data type
        // Is recommended to use typed methods (like etc.) instead, when possible.
        // 
        // [altair] 12.6.2004 Created
        // 
        public void SetProperty(int PID, byte[] Data, ExifDataTypes Type)
        {
            var P = _image.PropertyItems[0];
            P.Id = PID;
            P.Value = Data;
            P.Type = (short) Type;
            P.Len = Data.Length;
            _image.SetPropertyItem(P);
        }

        // 
        // Reads Int32 from EXIF bytearray.
        // 
        // EXIF bytearray to process
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // [altair] 05.09.2005 Changed from public shared to private instance method
        // 
        private int GetInt32(byte[] B)
        {
            if (B.Length < 4)
                throw new ArgumentException("Data too short (4 bytes expected)", "B");

            return B[3] << 24 | B[2] << 16 | B[1] << 8 | B[0];
        }

        // 
        // Reads Int16 from EXIF bytearray.
        // 
        // EXIF bytearray to process
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // [altair] 05.09.2005 Changed from public shared to private instance method
        // 
        private short GetInt16(byte[] B)
        {
            if (B.Length < 2)
                throw new ArgumentException("Data too short (2 bytes expected)", "B");

            return (short) (B[1] << 8 | B[0]);
        }

        // 
        // Reads string from EXIF bytearray.
        // 
        // EXIF bytearray to process
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // [altair] 05.09.2005 Changed from public shared to private instance method
        // 
        private string GetString(byte[] B)
        {
            var R = _encoding.GetString(B);
            if (R.EndsWith("\0"))
                R = R.Substring(0, R.Length - 1);
            return R;
        }

        // 
        // Reads rational from EXIF bytearray.
        // 
        // EXIF bytearray to process
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // [altair] 05.09.2005 Changed from public shared to private instance method
        // 
        private Rational GetRational(byte[] B)
        {
            var R = new Rational();
            var N = new byte[4];
            var D = new byte[4];
            Array.Copy(B, 0, N, 0, 4);
            Array.Copy(B, 4, D, 0, 4);
            R.Denominator = GetInt32(D);
            R.Numerator = GetInt32(N);
            return R;
        }

        #endregion

        //" IDisposable implementation "

        #region " IDisposable implementation "

        // 
        // Disposes unmanaged resources of this class
        // 
        // 
        // 
        // [altair] 10.09.2003 Created
        // 
        public void Dispose()
        {
            _image.Dispose();
        }

        #endregion

    }

}
