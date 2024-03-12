
function showGraph(graph) {
    function tick() {
        link
        .attr("x1", d => d.source.x)
        .attr("y1", d => d.source.y)
        .attr("x2", d => d.target.x)
        .attr("y2", d => d.target.y);
        node
        .attr("transform", (d, i) => `translate(${d.x - (d.name.length * 6)}, ${d.y - 8})`)
        // .attr("x", d => d.x - 15)
        // .attr("y", d => d.y - 5);
    }

    function click(event, d) {
        delete d.fx;
        delete d.fy;
        d3.select(this).classed("fixed", false);
        simulation.alpha(1).restart();
    }

    function dragstart() {
        d3.select(this).classed("fixed", true);
    }
    
    function dragged(event, d) {
        d.fx = clamp(event.x, 0, width);
        d.fy = clamp(event.y, 0, height);
        simulation.alpha(1).restart();
    }
    
    function clamp(x, lo, hi) {
        return x < lo ? lo : x > hi ? hi : x;
    }

    const height = 500;
    const width = 500;

    const svg = d3.create("svg").attr("viewBox", [0, 0, width, height]),
        link = svg
        .selectAll(".link")
        .data(graph.links)
        .join("line")
        .classed("link", true);
        
    const node = svg
        .selectAll(".node")
        .data(graph.nodes)
        .join("g")

        .classed("node", true)
        .classed("fixed", d => d.fx !== undefined)
        // .join("g")
        
        // .attr("foo", d => d.name)
        // .append("circle")
        // .attr("r", 6)
        // .attr("title", d => d.name)
        // .classed("node", true)
        // .classed("fixed", d => d.fx !== undefined)
    ;

    node.append("rect")
        .attr("width", d => d.name.length * 12)
        .attr("height", 16)
    ;

    node.append("text")
        .attr("dy", ".80em")
        .attr("dx", ".40em")
        .classed("text", true)
        .text(d => d.name)
    ;

    const simulation = d3
        .forceSimulation()
        .nodes(graph.nodes)
        .force("charge", d3.forceManyBody())
        .force("center", d3.forceCenter(width / 2, height / 2))
        .force("link", d3.forceLink(graph.links))
        .on("tick", tick);

    const drag = d3
        .drag()
        .on("start", dragstart)
        .on("drag", dragged);

    node.call(drag).on("click", click);

    container.append(svg.node());
}

  





