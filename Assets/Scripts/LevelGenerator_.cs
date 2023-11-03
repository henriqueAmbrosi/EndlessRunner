using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator_ : MonoBehaviour
{

    public Section_[] sectionPrefabs;
    public List<Section_> sections = new List<Section_>();
    public int totalSections = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLevelSection(false);
        SpawnLevelSection(false);
    }

    public void SpawnLevelSection(bool deleteFirstSection)
    {
        int randomIndex = Random.Range(0, sectionPrefabs.Length);
        Section_ currentSection = Instantiate(sectionPrefabs[randomIndex], new Vector3(0,0, totalSections*100), Quaternion.identity);
        sections.Add(currentSection);
        totalSections++;

        if (deleteFirstSection)
        {
            Destroy(sections[0].gameObject);
            sections.RemoveAt(0);
        }
    }
}
