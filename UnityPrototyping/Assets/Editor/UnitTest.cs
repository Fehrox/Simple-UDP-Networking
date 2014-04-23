using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class UnitTests : MonoBehaviour {

    [MenuItem("UnitTests/Run Unit Tests")]
    public static void RunUnitTest() {

        // Find all of our testable classes.
        var unitTestableType = typeof(IUnitTestable);
        var testableClasses = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => unitTestableType.IsAssignableFrom(p) && p != unitTestableType);

        // Run tests on all our testable classes.
        foreach (Type t in testableClasses) {
            if (!t.IsAbstract) {
                var test = Activator.CreateInstance(t) as IUnitTestable;
                var passed = false;
                try {
                    passed = test.RunTest();
                } finally {
                    // Report Results.
                    if(passed)
                        Debug.Log("Passed - " + t.Name);
                    else 
                        Debug.LogError("Failed - " + t.Name);
                }
            }
        }

    }
}
