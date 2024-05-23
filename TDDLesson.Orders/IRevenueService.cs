namespace TDDLesson;

public interface IRevenueService
{
    /// <summary>
    /// Возвращаем процент выручки по IT в этой компании
    /// </summary>
    /// <param name="companyNumber"></param>
    /// <returns>Процент от 0 до 1</returns>
    public float GetRevenuePercent(int companyNumber);
}