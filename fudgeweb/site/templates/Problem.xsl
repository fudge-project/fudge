<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <span class="title">Description</span>
    <div class="problem-desc">
      <xsl:value-of select="problem/description" disable-output-escaping="yes"/>
    </div>
    <span class="title">Input</span>
    <div class="problem-desc">
      <xsl:value-of select="problem/input" disable-output-escaping="yes"/>
    </div>
    <span class="title">Output</span>
    <div class="problem-desc">
      <xsl:value-of select="problem/output" disable-output-escaping="yes"/>
    </div>
    <span class="title">Sample Test Cases</span>
    <table class="testcases">
      <tr>
        <th>Input</th>
        <th>Expected Output</th>
      </tr>
      <xsl:for-each select="problem/cases/case/sample">
        <tr>
          <td>
            <pre>
              <xsl:value-of select="input" disable-output-escaping="no"/>
            </pre>
          </td>
          <td>
            <pre>
            <xsl:value-of select="output" disable-output-escaping="no"/>
            </pre>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>

