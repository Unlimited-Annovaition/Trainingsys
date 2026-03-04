namespace Application.DTOS;

public class TraineeResponseDto
{
    public int TraineeId { get; set; }
    public string FullName { get; set; } 
    public string PhoneNumber { get; set; } 
    public string Email { get; set; }
}
public class TraineeResponseDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int UserId { get; set; } 
}