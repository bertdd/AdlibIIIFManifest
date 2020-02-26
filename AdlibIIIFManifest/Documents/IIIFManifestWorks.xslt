<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <!-- NOTE: trying to use method=XML then output=JSON in api call doesnt work -->
  <!-- The XSLT is ignored in that use so a text ouput is the best method -->
  <xsl:output encoding="UTF-8" method="text"/>

	 <xsl:template match="adlibXML/recordList">
		<xsl:apply-templates select="record"/>   
	</xsl:template>
	
	<!-- Empty template to suppress the diagnostic element in response -->
	<xsl:template match="adlibXML/diagnostic"/>
	
	<xsl:template match="record">
		<!-- Noting the JSON nesting structure here for reference -->
		<!-- {[{[]}]} -->
		<!-- Boilerplate here, plus the Manifest ID - use local server path, but replace with API path when API solution is available -->
		<xsl:text>{"@context": "http://iiif.io/api/presentation/2/context.json","@type": "sc:Manifest","@id": "http://bk-img-data1:3000/works/</xsl:text>
		<xsl:value-of select="priref"/>
		<!-- Lebel - use title plus director and date where present -->
		<xsl:text>/manifest.json","label": "</xsl:text>
		<xsl:value-of select="Title[1]/title"/>
		<xsl:if test="count(credits[credit.type='Director']) &gt; 0 or count(Title_date) &gt; 0">
			<xsl:text> (</xsl:text>
				<xsl:if test="count(credits[credit.type='Director']) &gt; 0">
					<xsl:value-of select="substring-after(credits[credit.type='Director']/credit.name, ', ')"/>
					<xsl:text> </xsl:text>
					<xsl:value-of select="substring-before(credits[credit.type='Director']/credit.name, ',')"/>
					<xsl:if test="count(Title_date) &gt; 0">
						<xsl:text>, </xsl:text>
						<xsl:value-of select="Title_date[1]/title_date_start"/>
					</xsl:if>
				</xsl:if>
				<xsl:if test="count(credits[credit.type='Director']) &lt; 1">
					<xsl:value-of select="Title_date[1]/title_date_start"/>
				</xsl:if>
			<xsl:text>)</xsl:text>
		</xsl:if>
		<!-- Description commented out due to single quotes in the text causing JSON parse error - TODO use <xsl:translate> to remove them? -->
		<!-- <xsl:text>","description": "</xsl:text> -->
		<!-- <xsl:value-of select="translate(Description/description,'&apos;','')"/> -->
		<xsl:text>","attribution": "BFI National Archive Special Collections","logo": "https://upload.wikimedia.org/wikipedia/en/thumb/7/74/British_Film_Institute_logo_Black_and_White_solid.jpg/220px-British_Film_Institute_logo_Black_and_White_solid.jpg",</xsl:text>
		<!-- Add AIS URL as a related - not sure this html embedding is working, TODO debug -->
		<xsl:text>"related": {"@id": "http://collections-search.bfi.org.uk/web/Details/ChoiceFilmWorks/</xsl:text>
		<xsl:value-of select="priref"/>
		<xsl:text>", "format": "application/html" },</xsl:text>
		<!-- Add some descriptive metadate key value pairs -->
		<xsl:text>"metadata": [{ "label": "Director", "value": "</xsl:text>
		<xsl:value-of select="credits[credit.type='Director']/credit.name"/>
		<xsl:text>" },{ "label": "Date", "value": "</xsl:text>
		<xsl:value-of select="Title_date/title_date_start"/>
		<xsl:text>" },{ "label": "Genre", "value": "</xsl:text>
		<xsl:value-of select="Content_genre/content.genre"/>
		<xsl:text>" },{ "label": "Subject", "value": "</xsl:text>
		<xsl:value-of select="Content_subject/content.subject"/>
		<xsl:text>" }],</xsl:text>
		<!-- Stert the Sequences and invoke template per Related Object link -->
		<xsl:text>"sequences": [{"@type": "sc:Sequence","canvases": [</xsl:text>
		<xsl:apply-templates select="Related_object[related_object.reference/Reproduction/reproduction.reference/imagen.media.largeimage_umid != '']"/>
		<xsl:text>]}]}</xsl:text>
	</xsl:template>
	
	<!-- Template for Imagen JPGs, using Imagen UMID to retrieve from Imagen API via Cantaloupe IIIF Image server -->
	<xsl:template match="Related_object[related_object.reference/Reproduction/reproduction.reference/imagen.media.largeimage_umid != '']">
		<xsl:text>{"@type": "sc:Canvas","@id": "http://bk-img-data1:3000/works</xsl:text>
		<xsl:value-of select="../priref"/>
		<xsl:text>/</xsl:text>
		<xsl:value-of select="related_object.reference/object_number"/>
		<xsl:text>/canvas/</xsl:text>
		<xsl:value-of select="related_object.reference/Reproduction/reproduction.reference/imagen.media.part"/>
		<xsl:text>","label": "</xsl:text>
		<xsl:value-of select="related_object.reference/object_number"/>
		<xsl:text> - </xsl:text>
		<xsl:value-of select="related_object.reference/object_category"/>
		<xsl:text>","width": 6000,"height": 8000,"images": [{"@type": "oa:Annotation","motivation": "sc:painting",</xsl:text>
		<!-- AGain ID uses local server path but should change to API URL path if API solution works out -->
		<xsl:text>"on": "http://bk-img-data1:3000/works</xsl:text>
		<xsl:value-of select="../priref"/>
		<xsl:text>/</xsl:text>
		<xsl:value-of select="related_object.reference/object_number"/>
		<xsl:text>/canvas/</xsl:text>
		<xsl:value-of select="related_object.reference/Reproduction/reproduction.reference/imagen.media.part"/>
		<!-- This images URL refers to the local server where our Cantaloupe IIIF image server is running -->
		<xsl:text>","resource": {"@type": "dctypes:Image","@id": "http://bk-ci-web:8182/iiif/2/</xsl:text>
		<xsl:value-of select="related_object.reference/Reproduction/reproduction.reference/imagen.media.largeimage_umid"/>
		<xsl:text>/full/full/0/default.jpg","service": {"@context":  "http://iiif.io/api/image/2/context.json",</xsl:text>
		<xsl:text>"@id": "http://bk-ci-web:8182/iiif/2/</xsl:text>
		<xsl:value-of select="related_object.reference/Reproduction/reproduction.reference/imagen.media.largeimage_umid"/>
		<xsl:text>","profile": "http://iiif.io/api/image/2/level2.json"}}}]}</xsl:text>
		<!-- Add a comma only where there is a following-sibling Related Object -->
		<xsl:if test="count(following-sibling::Related_object[related_object.reference/Reproduction/reproduction.reference/imagen.media.largeimage_umid != '']) > 0">
			<xsl:text>,</xsl:text>
		</xsl:if>
	</xsl:template>
	
	<!-- Commented out as it is generating an API error - unexplained 'expected token )' in template match line -->
	<!-- Template for adding CID Media Server JPGs to manifest for Universal Viewer display, using GET CONTENT API command -->
	<!-- <xsl:template match="Related_object[related_object.reference/Reproduction/reproduction.reference/reproduction.type = 'Proxy']">
		<xsl:text>{"@type": "sc:Canvas","@id": "http://bk-img-data1:3000/works</xsl:text>
		<xsl:value-of select="../priref"/>
		<xsl:text>/</xsl:text>
		<xsl:value-of select="related_object.reference/object_number"/>
		<xsl:text>/canvas/</xsl:text>
		<xsl:value-of select="related_object.reference/Reproduction/reproduction.reference/imagen.media.part"/>
		<xsl:text>","label": "</xsl:text>
		<xsl:value-of select="related_object.reference/object_number"/>
		<xsl:text> - </xsl:text>
		<xsl:value-of select="related_object.reference/object_category"/>
		<xsl:text>","width": 6000,"height": 8000,"images": [{"@type": "oa:Annotation","motivation": "sc:painting",</xsl:text>
		<xsl:text>"on": "http://bk-img-data1:3000/works</xsl:text>
		<xsl:value-of select="../priref"/>
		<xsl:text>/</xsl:text>
		<xsl:value-of select="related_object.reference/object_number"/>
		<xsl:text>/canvas/</xsl:text>
		<xsl:value-of select="related_object.reference/Reproduction/reproduction.reference/imagen.media.part"/>
		<xsl:text>","resource": {"@type": "dctypes:Image","@id": "http://212.114.101.119/CIDData/wwwopac.ashx?command=getcontent&amp;server=images&amp;value=</xsl:text>
		<xsl:value-of select="related_object.reference/Reproduction/reproduction.reference"/>
		<xsl:text>"}}]}</xsl:text>
		<xsl:if test="count(following-sibling::Related_object[related_object.reference/Reproduction/reproduction.reference/reproduction.type = 'Proxy']">
			<xsl:text>,</xsl:text>
		</xsl:if>
	</xsl:template> -->
	
</xsl:stylesheet>