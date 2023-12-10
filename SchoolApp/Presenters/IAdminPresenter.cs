using SchoolApp.Models.DTOs;

namespace SchoolApp.Presenters;

public interface IAdminPresenter
{
    public void HandlePresenter(AdminDto adminDto);
}