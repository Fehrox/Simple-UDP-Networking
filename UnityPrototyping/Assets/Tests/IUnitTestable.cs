
public interface IUnitTestable {
#if UNITY_EDITOR
    bool RunTest();
#endif
}
