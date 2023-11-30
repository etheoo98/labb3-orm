namespace SchoolApp.Presenters;

public interface ILoginPresenter : IPresenter
{
    void OnSelect_Username();
    void OnSelect_Password();
    void OnSelect_Login();
}