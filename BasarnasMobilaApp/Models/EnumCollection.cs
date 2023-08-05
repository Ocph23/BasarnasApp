namespace BasarnasMobilaApp.Models
{
    public enum JenisKelamin
    {
        Pria,
        Wanita
    }

    public enum StatusDalamKeluarga
    {
        KepalaKeluarga,
        Istri,
        OrangTua,
        Mertua,
        Anak,
        Saudara,
        Ponakan,
        Dll,
        Pindah = 100,
        Meninggal = 101
    }
    public enum StatusPernikahan
    {
        Belum,
        Menikah,
        Duda,
        Janda
    }
    public enum Pekerjaan
    {
        None = 0,
        PNS,
        TNI,
        POLRI,
        Swasta,
        Petani,
        Nelayan,
        Peternak,
        Dll
    }

    public enum Pendidikan
    {
        None = 0,
        SD,
        SLTP,
        SLTA,
        Diploma,
        Sarjana,
    }


    public enum JenisUnsur
    {
        Keluarga, PW, PKB, PAR, None
    }

    public enum JabatanMajelis
    {
        Pendeta, Penatua, Syamas, Pengajar, Penginjil, Koster, Ketua, Sekertaris
    }

}
