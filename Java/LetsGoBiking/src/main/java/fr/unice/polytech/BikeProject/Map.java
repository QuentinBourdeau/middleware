package fr.unice.polytech.BikeProject;

import generated.ArrayOffloat;
import generated.Itinerary;
import org.jxmapviewer.JXMapViewer;
import org.jxmapviewer.OSMTileFactoryInfo;
import org.jxmapviewer.input.CenterMapListener;
import org.jxmapviewer.input.PanKeyListener;
import org.jxmapviewer.input.PanMouseInputListener;
import org.jxmapviewer.input.ZoomMouseWheelListenerCursor;
import org.jxmapviewer.painter.CompoundPainter;
import org.jxmapviewer.painter.Painter;
import org.jxmapviewer.viewer.*;
import javax.swing.*;
import javax.swing.event.MouseInputListener;
import java.util.*;

public class Map {
    private JXMapViewer mapViewer;
    private JFrame frame;
    private DefaultTileFactory tileFactory;
    private List<GeoPosition> geoPosition = new ArrayList<>();
    private RoutePainter routePainter;
    private WaypointPainter<Waypoint> waypointPainter = new WaypointPainter<Waypoint>();
    MouseInputListener mia;

    public Map(){
        mapViewer = new JXMapViewer();
        frame = new JFrame("Open Street Map");
        // Display the viewer in a JFrame
        frame.getContentPane().add(mapViewer);
        frame.setSize(800, 600);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setVisible(true);
        // Create a TileFactoryInfo for OpenStreetMap
        TileFactoryInfo info = new OSMTileFactoryInfo();
        tileFactory = new DefaultTileFactory(info);
        mapViewer.setTileFactory(tileFactory);
        mia = new PanMouseInputListener(mapViewer);
        mapViewer.addMouseListener(mia);
        mapViewer.addMouseMotionListener(mia);
        mapViewer.addMouseListener(new CenterMapListener(mapViewer));
        mapViewer.addMouseWheelListener(new ZoomMouseWheelListenerCursor(mapViewer));
        mapViewer.addKeyListener(new PanKeyListener(mapViewer));
    }

    public void setGeoPosition(Itinerary itinerary){
        for(ArrayOffloat f : itinerary.getCoordinates().getValue().getArrayOffloat()) {
            geoPosition.add(new GeoPosition(f.getFloat().get(1), f.getFloat().get(0)));
        }
        routePainter = new RoutePainter(geoPosition);

        // Set the focus
        mapViewer.zoomToBestFit(new HashSet<GeoPosition>(geoPosition), 0.7);

        // Create waypoints from the geo-positions
        Set<Waypoint> waypoints = new HashSet<Waypoint>(Arrays.asList(
                new DefaultWaypoint(geoPosition.get(0)),
                new DefaultWaypoint(geoPosition.get(geoPosition.size()-1))));

        // Create a waypoint painter that takes all the waypoints
        waypointPainter.setWaypoints(waypoints);
    }

    public void draw() {
        // Create a compound painter that uses both the route-painter and the waypoint-painter
        List<Painter<JXMapViewer>> painters = new ArrayList<Painter<JXMapViewer>>();
        painters.add(routePainter);
        painters.add(waypointPainter);
        CompoundPainter<JXMapViewer> painter = new CompoundPainter<JXMapViewer>(painters);
        mapViewer.setOverlayPainter(painter);
    }
}