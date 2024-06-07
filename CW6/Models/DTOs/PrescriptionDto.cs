namespace CW6.Models.DTOs
{
    public class PrescriptionDto
    {
        public PatientDto Patient { get; set; }
        public DoctorDto Doctor { get; set; }
        public List<MedicamentDto> Medicaments { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public PrescriptionDto()
        {
            Medicaments = new List<MedicamentDto>();
        }
    }
}
